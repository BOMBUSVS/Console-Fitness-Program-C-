﻿
using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using FitnessCaloriesProgram.BL.Model;

namespace FitnessCaloriesProgram.BL.Controller
{
    /// <summary>
    /// Контроллер пользователя.
    /// </summary>
    public class UserController : ControllerBase
    {
        private const string USERS_FILE_NAME = "users.dat";
        /// <summary>
        /// Пользователь приложения.
        /// </summary>
        public List<User> Users { get; }

        public User CurrentUser { get; }

        public bool IsNewUser { get; } = false;

        /// <summary>
        /// Создание нового контроллера пользователя.
        /// </summary>
        /// <param name="user"></param>

        public UserController(string userName)
        {
            if (string.IsNullOrWhiteSpace(userName))
            {
                throw new ArgumentNullException("Имя пользователя не может быть пустым", nameof(userName));
            }

            Users = GetUsersData();

            CurrentUser = Users.SingleOrDefault(u => u.Name == userName);

            if (CurrentUser == null)
            
            {
                CurrentUser = new User(userName);
                Users.Add(CurrentUser);
                IsNewUser = true;
            }

        }

        /// <summary>
        /// Получить сохраненный список пользователей.
        /// </summary>
        /// <returns></returns>
        private List<User> GetUsersData()
        {
            return Load<User>() ?? new List<User>();
        }

        public void SetNewUserData(string genderName, DateTime birthDate, double weight = 1, double height = 1)
        {
            // Проверка
            CurrentUser.Gender = new Gender(genderName);
            CurrentUser.BirthDate = birthDate;
            CurrentUser.Weight = weight;
            CurrentUser.Height = height;
            Save();
        }

        /// <summary>
        ///  Получить данные пользователя.
        /// </summary>
        /// <returns> Пользователь приложения.</returns>

        public void Save()
        {
            Save(Users);
        }
    }
}