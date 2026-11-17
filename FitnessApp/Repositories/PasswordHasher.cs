using System.Security.Cryptography;
using System.Text;

namespace FitnessApp.Repositories
{
    public static class PasswordHasher
    {
        public static string ComputeSha256Hash(string rawData)
        {
            // Создаем экземпляр SHA256
            using (SHA256 sha256Hash = SHA256.Create())
            {
                // Вычисляем хэш из байтов исходной строки
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));

                // Конвертируем байты в hex-строку
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }
    }
}