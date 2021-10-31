using ApplicationCore.Entities;
using ApplicationCore.Models;
using ApplicationCore.RepositoryInterfaces;
using ApplicationCore.ServiceInterfaces;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<int> RegisterUser(UserRegisterRequestModel requestModel)
        {
            // check whether email exists in the database
            var dbUser = await _userRepository.GetUserByEmail(requestModel.Email);
            if (dbUser != null)
                //email exists in the database
                throw new Exception("Email already exists, please login");

            // generate a random unique salt
            var salt = GetSalt();

            // create the hashed password with salt generated in the above step
            var hashedPassword = GetHashedPassword(requestModel.Password, salt);

            // save the user object to db
            // create user entity object
            var user = new User
            {
                FirstName = requestModel.FirstName,
                LastName = requestModel.LastName,
                Email = requestModel.Email,
                Salt = salt,
                HashedPassword = hashedPassword,
                DateOfBirth = requestModel.DateOfBirth
            };

            // use EF to save this user in the user table
            var newUser = await _userRepository.Add(user);
            return newUser.Id;
        }

        private string GetSalt()
        {
            var randomBytes = new byte[128 / 8];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomBytes);
            }

            return Convert.ToBase64String(randomBytes);
        }

        private string GetHashedPassword(string password, string salt)
        {
            var hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password,
                Convert.FromBase64String(salt),
                KeyDerivationPrf.HMACSHA512,
                10000,
                256 / 8));
            return hashed;
        }

        public async Task<UserLoginResponseModel> LoginUser(UserLoginRequestModel requestModel)
        {
            // get the salt and hashedpassword from databse for this user
            var dbUser = await _userRepository.GetUserByEmail(requestModel.Email);
            if (dbUser == null) throw null;

            // hash the user entered password with salt from the database

            var hashedPassword = GetHashedPassword(requestModel.Password, dbUser.Salt);
            // check the hashedpassword with database hashed password
            if (hashedPassword == dbUser.HashedPassword)
            {
                // user entered correct password
                var userLoginResponseModel = new UserLoginResponseModel
                {
                    Id = dbUser.Id,
                    FirstName = dbUser.FirstName,
                    LastName = dbUser.LastName,
                    DateOfBirth = dbUser.DateOfBirth.GetValueOrDefault(),
                    Email = dbUser.Email
                };
                return userLoginResponseModel;
            }

            return null;
        }

        public async Task AddFavorite(FavoriteRequestModel favoriteRequest)
        {
            var dbUser = await _userRepository.GetById(favoriteRequest.UserId);
            if (dbUser == null)
            {
                throw new Exception($"No User Found for this {favoriteRequest.UserId}");
            }

            var addFav = new FavoriteRequestModel
            {
                UserId = favoriteRequest.UserId,
                MovieId = favoriteRequest.MovieId
            };
        }

        public async Task RemoveFavorite(FavoriteRequestModel favoriteRequest)
        {
            var dbUser = await _userRepository.GetById(favoriteRequest.UserId);
            if (dbUser == null)
            {
                throw new Exception($"No User Found for this {favoriteRequest.UserId}");
            }

            var remFav = new FavoriteRequestModel
            {
                UserId = favoriteRequest.UserId,
                MovieId = favoriteRequest.MovieId
            };
        }

        public async Task<FavoriteResponseModel> GetAllFavoritesForUser(int id)
        {
            FavoriteResponseModel favoriteResponse = new FavoriteResponseModel();
            var dbUser = await _userRepository.GetById(id);
            //if (dbUser == null) throw null;
            //FavoriteResponseModel favoriteResponse = new FavoriteResponseModel();
            //List<FavoriteResponseModel.FavoriteMovieResponseModel> favoriteMovies = favoriteResponse.FavoriteMovies;
            //var movie = await _userRepository.GetById(id);
            if (dbUser == null)
            {
                throw new Exception($"No User Found for this {id}");
            }

            var favMovies = new FavoriteResponseModel
            {
                UserId = dbUser.Id,
                FavoriteMovies = favoriteResponse.FavoriteMovies
            };
            return favMovies;
        }

        public async Task<bool> PurchaseMovie(PurchaseRequestModel purchaseRequest, int userId)
        {
            var dbPurchase = await _userRepository.GetById(userId);
            if (dbPurchase == null)
            {
                throw new Exception($"No User Found for this {userId}");
            }
            if (purchaseRequest.PurchaseNumber==null)
            {
                var purchaseMovie = new PurchaseRequestModel
                {
                    MovieId = purchaseRequest.MovieId

                };
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<bool> IsMoviePurchased(PurchaseRequestModel purchaseRequest, int userId)
        {
            var dbPurchase = await _userRepository.GetById(userId);
            if (dbPurchase == null)
            {
                throw new Exception($"No User Found for this {userId}");
            }
            if (purchaseRequest.PurchaseNumber == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public async Task<PurchaseResponseModel> GetAllPurchasesForUser(int id)
        {
            PurchaseResponseModel favoriteResponse = new();
            var dbUser = await _userRepository.GetById(id);
            if (dbUser == null)
            {
                throw new Exception($"No User Found for this {id}");
            }

            var purchasedMovies = new PurchaseResponseModel
            {
                UserId = dbUser.Id,
                PurchasedMovies = favoriteResponse.PurchasedMovies
            };
            return purchasedMovies;
        }

        public async Task<PurchaseDetailsResponseModel> GetPurchasesDetails(int userId, int movieId)
        {
            PurchaseDetailsResponseModel favoriteResponse = new();
            var dbUser = await _userRepository.GetById(userId);
            if (dbUser == null)
            {
                throw new Exception($"No User Found for this {userId}");
            }

            var getPurchaseDetails = new PurchaseDetailsResponseModel
            {
                Id = favoriteResponse.Id,
                UserId = favoriteResponse.UserId,
                PurchaseNumber = favoriteResponse.PurchaseNumber,
                TotalPrice = favoriteResponse.TotalPrice,
                PurchaseDateTime = favoriteResponse.PurchaseDateTime,
                MovieId = favoriteResponse.MovieId,
                Title = favoriteResponse.Title,
                PosterUrl = favoriteResponse.PosterUrl,
                ReleaseDate = favoriteResponse.ReleaseDate

            };
            return getPurchaseDetails;
        }

        public async Task AddMovieReview(ReviewRequestModel reviewRequest)
        {
            var dbReview = await _userRepository.GetReviewsByUser(reviewRequest.UserId);
            if (dbReview == null)
            {
                throw new Exception($"No User Found for this {reviewRequest.UserId}");
            }

            var addReview = new ReviewRequestModel
            {
                UserId = reviewRequest.UserId,
                MovieId = reviewRequest.MovieId,
                ReviewText = reviewRequest.ReviewText,
                Rating = reviewRequest.Rating
            };
        }

        public async Task UpdateMovieReview(ReviewRequestModel reviewRequest)
        {
            var dbUpdate = await _userRepository.GetById(reviewRequest.UserId);
            if (dbUpdate == null)
            {
                throw new Exception($"No User Found for this {reviewRequest.UserId}");
            }

            var updateReview = new ReviewRequestModel
            {
                UserId = reviewRequest.UserId,
                MovieId = reviewRequest.MovieId,
                ReviewText = reviewRequest.ReviewText,
                Rating = reviewRequest.Rating
            };
        }

        public async Task DeleteMovieReview(int userId, int movieId)
        {

            var dbDelete = await _userRepository.GetById(userId);
            if (dbDelete == null)
            {
                throw new Exception($"No User Found for this {userId}");
            }

            var deleteReview = new ReviewRequestModel
            {
                UserId = userId,
                MovieId = movieId
            };
        }


    }
}
