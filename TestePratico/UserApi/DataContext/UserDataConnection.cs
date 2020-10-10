using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using UserApi.Model;

namespace UserApi.DataContext
{
    public class UserDataConnection
    {
        private SqlConnection _sql;

        public UserDataConnection()
        {
            //pega a connection string pelo arquivo ConnectionString.txt na raiz da solução
            var pathCs = Path.Combine(Directory.GetCurrentDirectory(), @"..\ConnectionString.txt");
            string _cs = File.ReadAllText(pathCs);

            _sql = new SqlConnection(_cs);
        }

        private bool Open()
        {
            if (_sql.State == System.Data.ConnectionState.Open)
                return true;

            try
            {
                _sql.Open();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        private bool Close()
        {
            if (_sql.State != System.Data.ConnectionState.Closed)
            {
                try
                {
                    _sql.Close();
                    return true;
                }
                catch (Exception e)
                {
                    return false;
                }
            }

            return true;
        }

        
        public List<User> GetAllUser()
        {
            List<User> users = new List<User>();

            string query = "SELECT * FROM USUARIO";

            if (!this.Open())
                return null;

            using (SqlCommand command = new SqlCommand(query, _sql))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    //pega todos os usuários no banco e coloca em uma lista 
                    while (reader.Read())
                    {
                        users.Add(new User()
                        {
                            Id = (long)reader["Id"],
                            Nome = (string)reader["Nome"],
                            CPF = (string)reader["CPF"],
                            Email = (string)reader["Email"],
                            Sexo = (string)reader["Sexo"],
                            Telefone = (string)reader["Telefone"],
                            DataNascimento = (DateTime)reader["DataNascimento"]
                        });
                    }
                }
            }

            this.Close();

            return users;
        }

        public User GetUserById(long id)
        {
            User user = new User();
            string query = "SELECT * FROM USUARIO WHERE Id = @Id";

            if (!this.Open())
                return null;
            using (SqlCommand command = new SqlCommand(query, _sql))
            {
                command.Parameters.AddWithValue("@Id", id);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    //seleciona um usuário pelo id e coloca em um objeto 
                    while (reader.Read())
                    {
                        user = new User()
                        {
                            Id = (long)reader["Id"],
                            Nome = (string)reader["Nome"],
                            CPF = (string)reader["CPF"],
                            Email = (string)reader["Email"],
                            Sexo = (string)reader["Sexo"],
                            Telefone = (string)reader["Telefone"],
                            DataNascimento = (DateTime)reader["DataNascimento"]
                        };
                    }
                }
            }
            this.Close();

            return user;

        }

        public void InsertUser(User newUser)
        {
            //insere um novo usuario pelos dados recebidos

            string query = @"INSERT INTO USUARIO (Nome, CPF, Email, Telefone, Sexo, DataNascimento)" +
                                            "VALUES (@Nome, @CPF, @Email, @Telefone, @Sexo, @DataNascimento) ";

            this.Open();
            using (SqlCommand command = new SqlCommand(query, _sql))
            {
                command.Parameters.AddWithValue("@Nome", newUser.Nome);
                command.Parameters.AddWithValue("@CPF", newUser.CPF);
                command.Parameters.AddWithValue("@Email", newUser.Email);
                command.Parameters.AddWithValue("@Telefone", newUser.Telefone);
                command.Parameters.AddWithValue("@Sexo", newUser.Sexo);
                command.Parameters.AddWithValue("@DataNascimento", newUser.DataNascimento);

                command.ExecuteNonQuery();
            }
            this.Close();
        }

        public void UpdateUser(User updateUser)
        {
            //realiza um update com base no objeto de usuário passado

            string query = @"UPDATE USUARIO SET
                                            Nome = @Nome,
                                            CPF = @CPF,
                                            Email = @Email,
                                            Telefone = @Telefone,
                                            Sexo = @Sexo,
                                            DataNascimento = @DataNascimento
                            WHERE Id = @Id";
            this.Open();
            using (SqlCommand command = new SqlCommand(query, _sql))
            {
                command.Parameters.AddWithValue("@Nome", updateUser.Nome);
                command.Parameters.AddWithValue("@CPF", updateUser.CPF);
                command.Parameters.AddWithValue("@Email", updateUser.Email);
                command.Parameters.AddWithValue("@Telefone", updateUser.Telefone);
                command.Parameters.AddWithValue("@Sexo", updateUser.Sexo);
                command.Parameters.AddWithValue("@DataNascimento", updateUser.DataNascimento);

                command.Parameters.AddWithValue("@Id", updateUser.Id);

                command.ExecuteNonQuery();
            }
            this.Close();
        }

        public void DeletUserById(long id)
        {
            //deleta um usuário pelo id passado
            string query = @"DELETE FROM USUARIO WHERE Id = @Id";

            this.Open();
            using (SqlCommand command = new SqlCommand(query, _sql))
            {
                command.Parameters.AddWithValue("@Id", id);
                command.ExecuteNonQuery();
            }
            this.Close();
        }
    }
}
