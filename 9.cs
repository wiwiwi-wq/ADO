//using System.Data;
//using System.Diagnostics;

////1
//using System;
//using System.Data.SqlClient;

//namespace SqlConnectionDemo
//{
//    class Program
//    {
//        static void Main(string[] args)
//        {
//            string connectionString = "Server=localhost;Database=master;Trusted_Connection=True;TrustServerCertificate=True;";

//            using (SqlConnection connection = new SqlConnection(connectionString))
//            {
//                try
//                {
//                    connection.Open();

//                    if (connection.State == System.Data.ConnectionState.Open)
//                    {
//                        Console.WriteLine("Подключение успешно установлено!");
//                        Console.WriteLine($"Сервер: {connection.ServerVersion}");
//                        Console.WriteLine($"База данных: {connection.Database}");
//                        Console.WriteLine($"Источник данных: {connection.DataSource}");
//                    }
//                }
//                catch (SqlException ex)
//                {
//                    Console.WriteLine("Ошибка SQL Server:");
//                    Console.WriteLine(ex.Message);
//                }
//                catch (Exception ex)
//                {
//                    Console.WriteLine("Ошибка подключения:");
//                    Console.WriteLine(ex.Message);
//                }
//            }

//            Console.WriteLine("Соединение закрыто.");
//            Console.ReadKey();
//        }
//    }
//}

////2
//using System;
//using System.Data.SqlClient;

//namespace SqlSelectDemo
//{
//    class Program
//    {
//        static void Main(string[] args)
//        {
//            string connectionString = "Server=localhost;Database=YourDatabase;Trusted_Connection=True;TrustServerCertificate=True;";

//            using (SqlConnection connection = new SqlConnection(connectionString))
//            {
//                try
//                {
//                    connection.Open();

//                    string query = "SELECT id, name, email FROM Users";

//                    using (SqlCommand command = new SqlCommand(query, connection))
//                    {
//                        using (SqlDataReader reader = command.ExecuteReader())
//                        {
//                            if (!reader.HasRows)
//                            {
//                                Console.WriteLine("Таблица Users пуста.");
//                                return;
//                            }

//                            Console.WriteLine("| {0,-5} | {1,-20} | {2,-30} |", "ID", "Name", "Email");
//                            Console.WriteLine(new string('-', 65));

//                            while (reader.Read())
//                            {
//                                int id = reader.GetInt32(0);
//                                string name = reader.IsDBNull(1) ? "" : reader.GetString(1);
//                                string email = reader.IsDBNull(2) ? "" : reader.GetString(2);

//                                Console.WriteLine("| {0,-5} | {1,-20} | {2,-30} |", id, name, email);
//                            }
//                        }
//                    }
//                }
//                catch (SqlException ex)
//                {
//                    Console.WriteLine("Ошибка SQL: " + ex.Message);
//                }
//                catch (Exception ex)
//                {
//                    Console.WriteLine("Ошибка: " + ex.Message);
//                }
//            }

//            Console.WriteLine("\nНажмите любую клавишу для завершения...");
//            Console.ReadKey();
//        }
//    }
//}

////3
//using System;
//using System.Data.SqlClient;

//namespace SqlDataReaderDemo
//{
//    class Program
//    {
//        static void Main(string[] args)
//        {
//            string connectionString = "Server=localhost;Database=YourDatabase;Trusted_Connection=True;TrustServerCertificate=True;";

//            using (SqlConnection connection = new SqlConnection(connectionString))
//            {
//                try
//                {
//                    connection.Open();

//                    string query = "SELECT id, name, email FROM Users";

//                    using (SqlCommand command = new SqlCommand(query, connection))
//                    {
//                        using (SqlDataReader reader = command.ExecuteReader())
//                        {
//                            if (!reader.HasRows)
//                            {
//                                Console.WriteLine("Таблица Users пуста.");
//                                Console.WriteLine("Прочитано записей: 0");
//                                Console.ReadKey();
//                                return;
//                            }

//                            Console.WriteLine("| {0,-5} | {1,-20} | {2,-30} |", "ID", "Name", "Email");
//                            Console.WriteLine(new string('-', 65));

//                            int rowCount = 0;

//                            while (reader.Read())
//                            {
//                                int id = reader.GetInt32(0);
//                                string name = reader.IsDBNull(1) ? "(null)" : reader.GetString(1);
//                                string email = reader.IsDBNull(2) ? "(null)" : reader.GetString(2);

//                                Console.WriteLine("| {0,-5} | {1,-20} | {2,-30} |", id, name, email);
//                                rowCount++;
//                            }

//                            Console.WriteLine(new string('-', 65));
//                            Console.WriteLine($"Прочитано записей: {rowCount}");
//                        }
//                    }
//                }
//                catch (SqlException ex)
//                {
//                    Console.WriteLine("Ошибка SQL: " + ex.Message);
//                }
//                catch (Exception ex)
//                {
//                    Console.WriteLine("Ошибка: " + ex.Message);
//                }
//            }

//            Console.WriteLine("\nНажмите любую клавишу для завершения...");
//            Console.ReadKey();
//        }
//    }
//}

////4
//using System;
//using System.Data;
//using System.Data.SqlClient;

//namespace SqlInsertDemo
//{
//    class Program
//    {
//        static void Main(string[] args)
//        {
//            string connectionString = "Server=localhost;Database=YourDatabase;Trusted_Connection=True;TrustServerCertificate=True;";

//            Console.Write("Введите имя: ");
//            string name = Console.ReadLine();

//            Console.Write("Введите email: ");
//            string email = Console.ReadLine();

//            InsertUser(connectionString, name, email);

//            Console.WriteLine("\nНажмите любую клавишу для завершения...");
//            Console.ReadKey();
//        }

//        static void InsertUser(string connectionString, string name, string email)
//        {
//            string query = "INSERT INTO Users (name, email) VALUES (@name, @email)";

//            using (SqlConnection connection = new SqlConnection(connectionString))
//            {
//                using (SqlCommand command = new SqlCommand(query, connection))
//                {
//                    command.Parameters.Add("@name", SqlDbType.NVarChar, 100).Value = name ?? (object)DBNull.Value;
//                    command.Parameters.Add("@email", SqlDbType.NVarChar, 255).Value = email ?? (object)DBNull.Value;

//                    try
//                    {
//                        connection.Open();
//                        int rowsAffected = command.ExecuteNonQuery();

//                        if (rowsAffected > 0)
//                        {
//                            Console.WriteLine("Запись успешно добавлена в таблицу Users.");
//                        }
//                    }
//                    catch (SqlException ex) when (ex.Number == 2627 || ex.Number == 2601)
//                    {
//                        Console.WriteLine("Ошибка: Нарушение ограничения уникальности (дубликат email или другого уникального поля).");
//                    }
//                    catch (SqlException ex) when (ex.Number == 547)
//                    {
//                        Console.WriteLine("Ошибка: Нарушение ограничения CHECK или внешнего ключа.");
//                    }
//                    catch (SqlException ex)
//                    {
//                        Console.WriteLine("Ошибка базы данных: " + ex.Message);
//                    }
//                    catch (Exception ex)
//                    {
//                        Console.WriteLine("Неизвестная ошибка: " + ex.Message);
//                    }
//                }
//            }
//        }
//    }
//}

////5
//using System;
//using System.Data;
//using System.Data.SqlClient;

//namespace SqlUpdateDemo
//{
//    class Program
//    {
//        static void Main(string[] args)
//        {
//            string connectionString = "Server=localhost;Database=YourDatabase;Trusted_Connection=True;TrustServerCertificate=True;";

//            Console.Write("Введите ID пользователя: ");
//            if (!int.TryParse(Console.ReadLine(), out int userId))
//            {
//                Console.WriteLine("Некорректный ID.");
//                Console.ReadKey();
//                return;
//            }

//            Console.Write("Введите новый email: ");
//            string newEmail = Console.ReadLine();

//            int updatedRows = UpdateUserEmail(connectionString, userId, newEmail);

//            if (updatedRows > 0)
//                Console.WriteLine($"Email успешно обновлён. Обновлено строк: {updatedRows}");
//            else
//                Console.WriteLine("Пользователь с указанным ID не найден.");

//            Console.WriteLine("\nНажмите любую клавишу для завершения...");
//            Console.ReadKey();
//        }

//        static int UpdateUserEmail(string connectionString, int userId, string newEmail)
//        {
//            string checkQuery = "SELECT COUNT(*) FROM Users WHERE id = @id";
//            string updateQuery = "UPDATE Users SET email = @email WHERE id = @id";

//            using (SqlConnection connection = new SqlConnection(connectionString))
//            {
//                connection.Open();

//                using (SqlCommand checkCmd = new SqlCommand(checkQuery, connection))
//                {
//                    checkCmd.Parameters.Add(new SqlParameter("@id", SqlDbType.Int) { Value = userId });
//                    int count = (int)checkCmd.ExecuteScalar();

//                    if (count == 0)
//                        return 0;
//                }

//                using (SqlCommand updateCmd = new SqlCommand(updateQuery, connection))
//                {
//                    updateCmd.Parameters.Add(new SqlParameter("@email", SqlDbType.NVarChar, 255) { Value = newEmail ?? (object)DBNull.Value });
//                    updateCmd.Parameters.Add(new SqlParameter("@id", SqlDbType.Int) { Value = userId });

//                    try
//                    {
//                        return updateCmd.ExecuteNonQuery();
//                    }
//                    catch (SqlException ex) when (ex.Number == 2627 || ex.Number == 2601)
//                    {
//                        Console.WriteLine("Ошибка: Новый email уже существует (нарушение уникальности).");
//                        return 0;
//                    }
//                    catch (SqlException ex)
//                    {
//                        Console.WriteLine("Ошибка базы данных: " + ex.Message);
//                        return 0;
//                    }
//                }
//            }
//        }
//    }
//}

////6
//using System;
//using System.Data;
//using System.Data.SqlClient;

//namespace SqlDeleteDemo
//{
//    class Program
//    {
//        static void Main(string[] args)
//        {
//            string connectionString = "Server=localhost;Database=YourDatabase;Trusted_Connection=True;TrustServerCertificate=True;";

//            Console.Write("Введите ID пользователя для удаления: ");
//            if (!int.TryParse(Console.ReadLine(), out int userId) || userId <= 0)
//            {
//                Console.WriteLine("Некорректный ID.");
//                Console.ReadKey();
//                return;
//            }

//            bool deleted = DeleteUserById(connectionString, userId);

//            if (deleted)
//                Console.WriteLine("Пользователь успешно удалён.");
//            else
//                Console.WriteLine("Удаление не выполнено (пользователь не найден или есть связанные данные).");

//            Console.WriteLine("\nНажмите любую клавишу для завершения...");
//            Console.ReadKey();
//        }

//        static bool DeleteUserById(string connectionString, int userId)
//        {
//            string checkQuery = "SELECT COUNT(*) FROM Users WHERE id = @id";
//            string deleteQuery = "DELETE FROM Users WHERE id = @id";

//            using (SqlConnection connection = new SqlConnection(connectionString))
//            {
//                try
//                {
//                    connection.Open();

//                    using (SqlCommand checkCmd = new SqlCommand(checkQuery, connection))
//                    {
//                        checkCmd.Parameters.Add(new SqlParameter("@id", SqlDbType.Int) { Value = userId });
//                        int count = (int)checkCmd.ExecuteScalar();

//                        if (count == 0)
//                        {
//                            Console.WriteLine("Пользователь с указанным ID не найден.");
//                            return false;
//                        }
//                    }

//                    using (SqlCommand deleteCmd = new SqlCommand(deleteQuery, connection))
//                    {
//                        deleteCmd.Parameters.Add(new SqlParameter("@id", SqlDbType.Int) { Value = userId });
//                        int rowsAffected = deleteCmd.ExecuteNonQuery();

//                        if (rowsAffected > 0)
//                        {
//                            Console.WriteLine($"Подтверждение: пользователь с ID = {userId} удалён.");
//                            return true;
//                        }
//                    }
//                }
//                catch (SqlException ex) when (ex.Number == 547)
//                {
//                    Console.WriteLine("Ошибка: Невозможно удалить пользователя — на него ссылаются другие таблицы (нарушение внешнего ключа).");
//                    return false;
//                }
//                catch (SqlException ex)
//                {
//                    Console.WriteLine("Ошибка базы данных: " + ex.Message);
//                    return false;
//                }
//                catch (Exception ex)
//                {
//                    Console.WriteLine("Неизвестная ошибка: " + ex.Message);
//                    return false;
//                }

//                return false;
//            }
//        }
//    }
//}

////7
//using System;
//using System.Data;
//using System.Data.SqlClient;

//namespace SqlInjectionDemo
//{
//    class Program
//    {
//        static string connectionString = "Server=localhost;Database=YourDatabase;Trusted_Connection=True;TrustServerCertificate=True;";

//        static void Main(string[] args)
//        {
//            Console.WriteLine("Демонстрация SQL Injection\n");

//            Console.Write("Введите email для поиска (безопасный метод): ");
//            string input = Console.ReadLine();

//            Console.WriteLine("\n1. Безопасный метод ( SqlParameter )");
//            SafeLoginSearch(input);

//            Console.WriteLine("\n2. Уязвимый метод (конкатенация строк)");
//            UnsafeLoginSearch(input);

//            Console.WriteLine("\nНажмите любую клавишу для завершения...");
//            Console.ReadKey();
//        }

//        // Безопасный метод — использует параметры
//        static void SafeLoginSearch(string email)
//        {
//            string query = "SELECT id, name, email FROM Users WHERE email = @email";

//            using (SqlConnection conn = new SqlConnection(connectionString))
//            {
//                using (SqlCommand cmd = new SqlCommand(query, conn))
//                {
//                    cmd.Parameters.Add(new SqlParameter("@email", SqlDbType.NVarChar, 255) { Value = email });

//                    try
//                    {
//                        conn.Open();
//                        using (SqlDataReader reader = cmd.ExecuteReader())
//                        {
//                            if (reader.Read())
//                                Console.WriteLine($"Найден: ID={reader["id"]}, Name={reader["name"]}, Email={reader["email"]}");
//                            else
//                                Console.WriteLine("Пользователь не найден.");
//                        }
//                    }
//                    catch (Exception ex) { Console.WriteLine("Ошибка: " + ex.Message); }
//                }
//            }

//            Console.WriteLine("Параметры полностью защищают от SQL-инъекций!");
//        }

//        static void UnsafeLoginSearch(string email)
//        {
//            string query = "SELECT id, name, email FROM Users WHERE email = '" + email + "'";

//            Console.WriteLine($"Выполняемый запрос: {query}");

//            using (SqlConnection conn = new SqlConnection(connectionString))
//            {
//                using (SqlCommand cmd = new SqlCommand(query, conn))
//                {
//                    try
//                    {
//                        conn.Open();
//                        using (SqlDataReader reader = cmd.ExecuteReader())
//                        {
//                            Console.WriteLine("Результаты (может быть много!):");
//                            while (reader.Read())
//                            {
//                                Console.WriteLine($"  → ID={reader["id"]}, Name={reader["name"]}, Email={reader["email"]}");
//                            }
//                        }
//                    }
//                    catch (Exception ex) { Console.WriteLine("Ошибка: " + ex.Message); }
//                }
//            }

//            Console.WriteLine("ВНИМАНИЕ: Если ввести ' OR '1'='1 — будут выведены ВСЕ пользователи!");
//            Console.WriteLine("Это и есть SQL Injection — злоумышленник может удалить данные, войти без пароля и т.д.");
//        }
//    }
//}

////8
//using System;
//using System.Data;
//using System.Data.SqlClient;

//namespace SqlDataAdapterDemo
//{
//    class Program
//    {
//        static void Main(string[] args)
//        {
//            string connectionString = "Server=localhost;Database=YourDatabase;Trusted_Connection=True;TrustServerCertificate=True;";

//            string query = "SELECT id, name, email FROM Users";

//            DataTable dataTable = new DataTable();

//            using (SqlConnection connection = new SqlConnection(connectionString))
//            {
//                using (SqlDataAdapter adapter = new SqlDataAdapter(query, connection))
//                {
//                    try
//                    {
//                        adapter.Fill(dataTable);

//                        if (dataTable.Rows.Count == 0)
//                        {
//                            Console.WriteLine("Таблица Users пуста.");
//                        }
//                        else
//                        {
//                            Console.WriteLine($"Загружено записей: {dataTable.Rows.Count}");
//                            Console.WriteLine();

//                            Console.WriteLine("| {0,-5} | {1,-20} | {2,-30} |", "ID", "Name", "Email");
//                            Console.WriteLine(new string('-', 65));

//                            foreach (DataRow row in dataTable.Rows)
//                            {
//                                int id = Convert.ToInt32(row["id"]);
//                                string name = row["name"] == DBNull.Value ? "(null)" : row["name"].ToString();
//                                string email = row["email"] == DBNull.Value ? "(null)" : row["email"].ToString();

//                                Console.WriteLine("| {0,-5} | {1,-20} | {2,-30} |", id, name, email);
//                            }

//                            Console.WriteLine(new string('-', 65));

//                            if (dataTable.Rows.Count > 0)
//                            {
//                                DataRow firstRow = dataTable.Rows[0];
//                                Console.WriteLine($"Первая строка: ID = {firstRow["id"]}, Name = {firstRow["name"]}");
//                            }
//                        }
//                    }
//                    catch (Exception ex)
//                    {
//                        Console.WriteLine("Ошибка: " + ex.Message);
//                    }
//                }
//            }

//            Console.WriteLine("\nНажмите любую клавишу для завершения...");
//            Console.ReadKey();
//        }
//    }
//}

////9
//using System;
//using System.Data;
//using System.Data.SqlClient;

//namespace DataSetRelationsDemo
//{
//    class Program
//    {
//        static void Main(string[] args)
//        {
//            string connectionString = "Server=localhost;Database=YourDatabase;Trusted_Connection=True;TrustServerCertificate=True;";

//            DataSet dataSet = new DataSet("Shop");

//            using (SqlConnection connection = new SqlConnection(connectionString))
//            {
//                try
//                {
//                    connection.Open();

//                    string sqlUsers = "SELECT id, name, email FROM Users";
//                    using (SqlDataAdapter adapterUsers = new SqlDataAdapter(sqlUsers, connection))
//                    {
//                        adapterUsers.Fill(dataSet, "Users");
//                    }

//                    string sqlOrders = "SELECT id, user_id, product, amount FROM Orders";
//                    using (SqlDataAdapter adapterOrders = new SqlDataAdapter(sqlOrders, connection))
//                    {
//                        adapterOrders.Fill(dataSet, "Orders");
//                    }

//                    DataRelation relation = new DataRelation(
//                        "UserOrders",
//                        dataSet.Tables["Users"].Columns["id"],
//                        dataSet.Tables["Orders"].Columns["user_id"]
//                    );

//                    dataSet.Relations.Add(relation);

//                    Console.WriteLine("=== Пользователи и их заказы ===\n");

//                    foreach (DataRow userRow in dataSet.Tables["Users"].Rows)
//                    {
//                        Console.WriteLine($"Пользователь: {userRow["id"]}. {userRow["name"]} ({userRow["email"]})");

//                        DataRow[] childOrders = userRow.GetChildRows(relation);

//                        if (childOrders.Length == 0)
//                        {
//                            Console.WriteLine("   → Нет заказов");
//                        }
//                        else
//                        {
//                            Console.WriteLine($"   → Заказов: {childOrders.Length}");
//                            foreach (DataRow order in childOrders)
//                            {
//                                Console.WriteLine($"     • Заказ #{order["id"]}: {order["product"]}, сумма: {order["amount"]}");
//                            }
//                        }
//                        Console.WriteLine();
//                    }

//                    Console.WriteLine($"Всего пользователей: {dataSet.Tables["Users"].Rows.Count}");
//                    Console.WriteLine($"Всего заказов: {dataSet.Tables["Orders"].Rows.Count}");
//                }
//                catch (Exception ex)
//                {
//                    Console.WriteLine("Ошибка: " + ex.Message);
//                }
//            }

//            Console.WriteLine("Нажмите любую клавишу для завершения...");
//            Console.ReadKey();
//        }
//    }
//}

////10
//using System;
//using System.Data;
//using System.Data.SqlClient;
//using System.Threading;

//namespace DatabaseExceptionHandlingDemo
//{
//    class Program
//    {
//        static string connectionString = "Server=localhost;Database=YourDatabase;Trusted_Connection=True;TrustServerCertificate=True;";

//        static void Main(string[] args)
//        {
//            Console.WriteLine("Демонстрация полной обработки исключений при работе с БД\n");

//            ExecuteWithFullExceptionHandling("SELECT * FROM Users");


//            Console.WriteLine("\nПрограмма завершена.");
//            Console.ReadKey();
//        }

//        static void ExecuteWithFullExceptionHandling(string query)
//        {
//            using (SqlConnection connection = new SqlConnection(connectionString))
//            {
//                SqlCommand command = new SqlCommand(query, connection);
//                command.CommandTimeout = 10; /

//                try
//                {
//                    Console.WriteLine($"Выполняется запрос: {query}");
//                    connection.Open();

//                    using (SqlDataReader reader = command.ExecuteReader())
//                    {
//                        int count = 0;
//                        while (reader.Read()) count++;
//                        Console.WriteLine($"Успешно получено строк: {count}");
//                    }
//                }
//                catch (SqlException sqlEx)
//                {
//                    LogError("SQL Server Error", sqlEx);
//                    HandleSqlException(sqlEx);
//                }
//                catch (TimeoutException timeoutEx)
//                {
//                    LogError("Timeout Error", timeoutEx);
//                    Console.WriteLine("Превышено время ожидания выполнения запроса (таймаут).");
//                    Console.WriteLine("Рекомендация: увеличьте CommandTimeout или оптимизируйте запрос.");
//                }
//                catch (InvalidOperationException invOpEx)
//                {
//                    LogError("Invalid Operation", invOpEx);
//                    Console.WriteLine("Недопустимая операция: возможно, соединение уже открыто/закрыто или объект использован неверно.");
//                }
//                catch (Exception ex)
//                {
//                    LogError("Unexpected Error", ex);
//                    Console.WriteLine($"Неизвестная ошибка: {ex.Message}");
//                }
//            }
//        }

//        static void HandleSqlException(SqlException sqlEx)
//        {
//            foreach (SqlError error in sqlEx.Errors)
//            {
//                Console.WriteLine($"  • Код ошибки: {error.Number}");
//                Console.WriteLine($"  • Уровень: {error.Class}");
//                Console.WriteLine($"  • Сообщение: {error.Message}");
//                Console.WriteLine($"  • Процедура: {error.Procedure}");
//                Console.WriteLine($"  • Сервер: {error.Server}");
//                Console.WriteLine($"  • Строка: {error.LineNumber}\n");
//            }

//            switch (sqlEx.Number)
//            {
//                case 2: Console.WriteLine("Сервер недоступен или неправильный адрес."); break;
//                case 53: Console.WriteLine("Сетевая ошибка подключения."); break;
//                case 18456: Console.WriteLine("Ошибка авторизации (неверный логин/пароль)."); break;
//                case 4060: Console.WriteLine("Не удалось открыть базу данных."); break;
//                case 207: Console.WriteLine("Неверное имя столбца."); break;
//                case 208: Console.WriteLine("Таблица не найдена."); break;
//                case 547: Console.WriteLine("Нарушение ограничения (внешний ключ, CHECK и т.д.)."); break;
//                case 2627: case 2601: Console.WriteLine("Нарушение уникальности (дубликат ключа)."); break;
//                case -2: Console.WriteLine("Таймаут подключения."); break;
//                default: Console.WriteLine("Другая ошибка SQL Server."); break;
//            }
//        }

//        static void LogError(string errorType, Exception ex)
//        {
//            string logMessage = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss} | {errorType} | {ex.GetType().Name} | {ex.Message}\n{ex.StackTrace}\n";
//            Console.WriteLine($"\n[ЛОГ] {logMessage}");
//        }
//    }
//}

////11
//using System;
//using System.Data;
//using System.Data.SqlClient;

//namespace StoredProcedureDemo
//{
//    class Program
//    {
//        static string connectionString = "Server=localhost;Database=YourDatabase;Trusted_Connection=True;TrustServerCertificate=True;";

//        static void Main(string[] args)
//        {
//            Console.Write("Введите ID пользователя: ");
//            if (!int.TryParse(Console.ReadLine(), out int userId) || userId <= 0)
//            {
//                Console.WriteLine("Некорректный ID.");
//                Console.ReadKey();
//                return;
//            }

//            GetUserOrders(userId);

//            Console.WriteLine("\nНажмите любую клавишу для завершения...");
//            Console.ReadKey();
//        }

//        static void GetUserOrders(int userId)
//        {
//            using (SqlConnection connection = new SqlConnection(connectionString))
//            {
//                using (SqlCommand command = new SqlCommand("GetUserOrders", connection))
//                {
//                    command.CommandType = CommandType.StoredProcedure;

//                    command.Parameters.Add(new SqlParameter("@UserId", SqlDbType.Int) { Value = userId });

//                    try
//                    {
//                        connection.Open();

//                        using (SqlDataReader reader = command.ExecuteReader())
//                        {
//                            if (!reader.HasRows)
//                            {
//                                Console.WriteLine($"У пользователя с ID = {userId} нет заказов.");
//                                return;
//                            }

//                            Console.WriteLine($"Заказы пользователя ID = {userId}:");
//                            Console.WriteLine("| {0,-5} | {1,-20} | {2,-12} |", "ID", "Товар", "Сумма");
//                            Console.WriteLine(new string('-', 50));

//                            while (reader.Read())
//                            {
//                                int orderId = reader.GetInt32("id");
//                                string product = reader.GetString("product");
//                                decimal amount = reader.GetDecimal("amount");

//                                Console.WriteLine("| {0,-5} | {1,-20} | {2,-12:C} |", orderId, product, amount);
//                            }
//                        }
//                    }
//                    catch (SqlException ex)
//                    {
//                        Console.WriteLine("Ошибка при выполнении хранимой процедуры:");
//                        Console.WriteLine(ex.Message);
//                    }
//                    catch (Exception ex)
//                    {
//                        Console.WriteLine("Ошибка: " + ex.Message);
//                    }
//                }
//            }
//        }
//    }
//}

////12
//using System;
//using System.Data;
//using System.Data.SqlClient;

//class Program
//{
//    static string connStr = "Server=localhost;Database=YourDatabase;Trusted_Connection=True;TrustServerCertificate=True;";

//    static void Main()
//    {
//        Console.Write("Введите ID пользователя: ");
//        int userId = int.Parse(Console.ReadLine());

//        using (SqlConnection conn = new SqlConnection(connStr))
//        {
//            SqlCommand cmd = new SqlCommand("GetUserOrdersCount", conn);
//            cmd.CommandType = CommandType.StoredProcedure;

//            cmd.Parameters.Add("@UserId", SqlDbType.Int).Value = userId;
//            SqlParameter outputParam = new SqlParameter("@OrdersCount", SqlDbType.Int)
//            {
//                Direction = ParameterDirection.Output
//            };
//            cmd.Parameters.Add(outputParam);

//            conn.Open();
//            cmd.ExecuteNonQuery();

//            int count = outputParam.Value == DBNull.Value ? 0 : (int)outputParam.Value;
//            Console.WriteLine($"Количество заказов пользователя {userId}: {count}");
//        }
//        Console.ReadKey();
//    }
//}

////13
//using System;
//using System.Data.SqlClient;

//class Program
//{
//    static string connStr = "Server=localhost;Database=YourDatabase;Trusted_Connection=True;TrustServerCertificate=True;";

//    static void Main()
//    {
//        TransferMoney(1, 2, 300.00m);
//        Console.ReadKey();
//    }

//    static bool TransferMoney(int fromId, int toId, decimal amount)
//    {
//        using (SqlConnection conn = new SqlConnection(connStr))
//        {
//            conn.Open();
//            using (SqlTransaction tx = conn.BeginTransaction())
//            {
//                try
//                {
//                    using (SqlCommand cmd = new SqlCommand(
//                        "UPDATE Accounts SET Balance = Balance - @amount WHERE Id = @fromId;" +
//                        "UPDATE Accounts SET Balance = Balance + @amount WHERE Id = @toId;",
//                        conn, tx))
//                    {
//                        cmd.Parameters.AddWithValue("@amount", amount);
//                        cmd.Parameters.AddWithValue("@fromId", fromId);
//                        cmd.Parameters.AddWithValue("@toId", toId);

//                        cmd.ExecuteNonQuery();
//                    }
//                    tx.Commit();
//                    Console.WriteLine($"Перевод {amount:C} успешен!");
//                    return true;
//                }
//                catch (Exception ex)
//                {
//                    tx.Rollback();
//                    Console.WriteLine("Ошибка! Транзакция отменена: " + ex.Message);
//                    return false;
//                }
//            }
//        }
//    }
//}

////14
//using System;
//using System.Data;
//using System.Data.SqlClient;
//using System.Threading.Tasks;

//class Program
//{
//    static string connStr = "Server=localhost;Database=YourDatabase;Trusted_Connection=True;TrustServerCertificate=True;";

//    static void Main()
//    {
//        DemoIsolationLevels();
//        Console.ReadKey();
//    }

//    static void DemoIsolationLevels()
//    {
//        using (SqlConnection conn = new SqlConnection(connStr))
//        {
//            conn.Open();

//            new SqlCommand("IF OBJECT_ID('TestTable') IS NULL CREATE TABLE TestTable (Id INT, Value INT); TRUNCATE TABLE TestTable; INSERT INTO TestTable VALUES (1, 100)", conn).ExecuteNonQuery();

//            Console.WriteLine("1. ReadUncommitted (грязное чтение):");
//            Task.Run(() => DirtyReadDemo(conn));

//            Console.WriteLine("\n2. ReadCommitted (по умолчанию):");
//            Task.Run(() => ReadCommittedDemo(conn));

//            Console.WriteLine("\n3. Serializable (полная блокировка):");
//            Task.Run(() => SerializableDemo(conn));

//            Console.ReadKey();
//        }
//    }

//    static void DirtyReadDemo(SqlConnection conn)
//    {
//        using (var c = new SqlConnection(connStr)) c.Open();
//        using (var tx = c.BeginTransaction(IsolationLevel.ReadUncommitted))
//        using (var cmd = c.CreateCommand()) { cmd.Transaction = tx; cmd.CommandText = "SELECT Value FROM TestTable WHERE Id=1"; Console.WriteLine("ReadUncommitted видит: " + cmd.ExecuteScalar()); }
//    }

//    static void ReadCommittedDemo(SqlConnection conn)
//    {
//        using (var c = new SqlConnection(connStr)) c.Open();
//        using (var tx = c.BeginTransaction(IsolationLevel.ReadCommitted))
//        using (var cmd = c.CreateCommand()) { cmd.Transaction = tx; cmd.CommandText = "SELECT Value FROM TestTable WHERE Id=1"; Console.WriteLine("ReadCommitted видит: " + cmd.ExecuteScalar()); }
//    }

//    static void SerializableDemo(SqlConnection conn)
//    {
//        using (var c = new SqlConnection(connStr)) c.Open();
//        using (var tx = c.BeginTransaction(IsolationLevel.Serializable))
//        using (var cmd = c.CreateCommand()) { cmd.Transaction = tx; cmd.CommandText = "SELECT Value FROM TestTable WHERE Id=1"; Console.WriteLine("Serializable — ждёт завершения других транзакций..."); cmd.ExecuteScalar(); }
//    }
//}

////15
//using System;
//using System.Data;
//using System.Data.SqlClient;
//using System.Diagnostics;

//class Program
//{
//    static string connStr = "Server=localhost;Database=YourDatabase;Trusted_Connection=True;TrustServerCertificate=True;";

//    static void Main()
//    {
//        DataTable table = new DataTable();
//        table.Columns.Add("Name", typeof(string));
//        table.Columns.Add("Email", typeof(string));

//        for (int i = 1; i <= 10000; i++)
//            table.Rows.Add("User" + i, $"user{i}@test.com");

//        Console.WriteLine("Тестируем обычный INSERT...");
//        var sw1 = Stopwatch.StartNew();
//        InsertOneByOne(table);
//        sw1.Stop();

//        new SqlCommand("TRUNCATE TABLE Users", new SqlConnection(connStr) { }.Open()).ExecuteNonQuery();

//        Console.WriteLine($"Обычный INSERT: {sw1.ElapsedMilliseconds} мс");

//        Console.WriteLine("Тестируем SqlBulkCopy...");
//        var sw2 = Stopwatch.StartNew();
//        BulkInsert(table);
//        sw2.Stop();

//        Console.WriteLine($"SqlBulkCopy: {sw2.ElapsedMilliseconds} мс (в {sw1.ElapsedMilliseconds / (double)sw2.ElapsedMilliseconds:F1} раз быстрее!)");

//        Console.ReadKey();
//    }

//    static void InsertOneByOne(DataTable data)
//    {
//        using (SqlConnection conn = new SqlConnection(connStr))
//        {
//            conn.Open();
//            foreach (DataRow row in data.Rows)
//            {
//                using (SqlCommand cmd = new SqlCommand("INSERT INTO Users (name, email) VALUES (@n, @e)", conn))
//                {
//                    cmd.Parameters.AddWithValue("@n", row["Name"]);
//                    cmd.Parameters.AddWithValue("@e", row["Email"]);
//                    cmd.ExecuteNonQuery();
//                }
//            }
//        }
//    }

//    static void BulkInsert(DataTable data)
//    {
//        using (SqlBulkCopy bulk = new SqlBulkCopy(connStr))
//        {
//            bulk.DestinationTableName = "Users";
//            bulk.ColumnMappings.Add("Name", "name");
//            bulk.ColumnMappings.Add("Email", "email");
//            bulk.BulkCopyTimeout = 300;
//            bulk.WriteToServer(data);
//        }
//    }
//}

////16
//using System;
//using System.ComponentModel;
//using System.Data;
//using System.Data.SqlClient;
//using System.Diagnostics;
//using System.Formats.Asn1;
//using System.Reflection;
//using System.Xml;

//public class EmployeeTableAdapter
//{
//    private string connectionString;

//    public EmployeeTableAdapter(string connectionString)
//    {
//        this.connectionString = connectionString;
//    }

//    // Загрузка данных в DataTable
//    public void Fill(EmployeesDataTable employees)
//    {
//        using (SqlConnection connection = new SqlConnection(connectionString))
//        {
//            connection.Open();
//            SqlCommand command = new SqlCommand("SELECT * FROM Employees", connection);
//            SqlDataAdapter adapter = new SqlDataAdapter(command);
//            adapter.Fill(employees);
//        }
//    }

//    // Обновление данных в базе
//    public void Update(EmployeesDataTable employees)
//    {
//        using (SqlConnection connection = new SqlConnection(connectionString))
//        {
//            connection.Open();
//            SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM Employees", connection);

//            // Настройка команд для обновления
//            SqlCommandBuilder builder = new SqlCommandBuilder(adapter);
//            adapter.UpdateCommand = builder.GetUpdateCommand();
//            adapter.InsertCommand = builder.GetInsertCommand();
//            adapter.DeleteCommand = builder.GetDeleteCommand();

//            adapter.Update(employees);
//        }
//    }
//}

////Пример использования для редактирования данных
//using System;

//class Program
//{
//    static void Main()
//    {
//        string connectionString = "Your_Connection_String";
//        EmployeeDataSet employeeDataSet = new EmployeeDataSet();
//        EmployeeTableAdapter adapter = new EmployeeTableAdapter(connectionString);

//        // Загрузка данных
//        adapter.Fill(employeeDataSet.Employees);

//        // Редактирование одной записи
//        EmployeeRow employee = employeeDataSet.Employees.FindByID(1);
//        if (employee != null)
//        {
//            employee.Name = "Иван Иванов";
//            employee.Salary = 50000;
//        }

//        // Массовое редактирование
//        foreach (EmployeeRow emp in employeeDataSet.Employees.GetActiveEmployees())
//        {
//            emp.Salary *= 1.1m; // Увеличение зарплаты на 10%
//        }

//        // Отчёт об изменениях
//        Console.WriteLine("Изменения перед сохранением:");
//        foreach (EmployeeRow emp in employeeDataSet.Employees.GetActiveEmployees())
//        {
//            if (emp.RowState == DataRowState.Modified)
//                Console.WriteLine($"ID: {emp.EmployeeID}, Новое имя: {emp.Name}, Новая зарплата: {emp.Salary:C}");
//        }

//        // Сохранение изменений
//        adapter.Update(employeeDataSet.Employees);
//        employeeDataSet.AcceptChanges();

//        Console.WriteLine("Изменения сохранены!");
//    }
//}



////17
//using System;

//class Program
//{
//    static void Main()
//    {
//        string connectionString = "Your_Connection_String";
//        EmployeeDataSet employeeDataSet = new EmployeeDataSet();
//        EmployeeTableAdapter adapter = new EmployeeTableAdapter(connectionString);

//        // Загрузка данных
//        adapter.Fill(employeeDataSet.Employees);

//        // Удаление одной записи
//        EmployeeRow employee = employeeDataSet.Employees.FindByID(2);
//        if (employee != null)
//        {
//            employee.Row.Delete();
//        }

//        // Массовое удаление
//        foreach (EmployeeRow emp in employeeDataSet.Employees.GetByDepartment("HR"))
//        {
//            emp.Row.Delete();
//        }

//        // Отчёт об удалённых данных
//        Console.WriteLine("Удаленные записи:");
//        foreach (DataRow row in employeeDataSet.Employees.Select(null, null, DataViewRowState.Deleted))
//        {
//            Console.WriteLine($"ID: {row["EmployeeID", DataRowVersion.Original]}");
//        }

//        // Сохранение удалений
//        adapter.Update(employeeDataSet.Employees);
//        employeeDataSet.AcceptChanges();

//        Console.WriteLine("Удаления сохранены!");
//    }
//}


////18
//using System;
//using System.Diagnostics;

//class Program
//{
//    static void Main()
//    {
//        string connectionString = "Your_Connection_String";
//        EmployeeDataSet employeeDataSet = new EmployeeDataSet();
//        EmployeeTableAdapter adapter = new EmployeeTableAdapter(connectionString);

//        // Загрузка данных
//        adapter.Fill(employeeDataSet.Employees);

//        // Добавление новой записи
//        employeeDataSet.Employees.AddEmployeeRow(
//            "Петр Петров",
//            "IT",
//            60000,
//            DateTime.Now,
//            "petr.petrov@example.com",
//            "Active"
//        );

//        // Редактирование записи
//        EmployeeRow employee = employeeDataSet.Employees.FindByID(3);
//        if (employee != null)
//        {
//            employee.Department = "Finance";
//        }

//        // Удаление записи
//        employee = employeeDataSet.Employees.FindByID(4);
//        if (employee != null)
//        {
//            employee.Row.Delete();
//        }

//        // Проверка изменений
//        DataTable changes = employeeDataSet.Employees.GetChanges();
//        if (changes != null)
//        {
//            Console.WriteLine($"Обнаружено {changes.Rows.Count} изменений.");
//            foreach (DataRow row in changes.Rows)
//            {
//                Console.WriteLine($"ID: {row["EmployeeID"]}, RowState: {row.RowState}");
//            }
//        }

//        // Синхронизация с БД
//        Stopwatch stopwatch = Stopwatch.StartNew();
//        adapter.Update(employeeDataSet.Employees);
//        stopwatch.Stop();

//        employeeDataSet.AcceptChanges();
//        Console.WriteLine($"Синхронизация завершена за {stopwatch.ElapsedMilliseconds} мс!");
//    }
//}


////19
//using System;
//using System.Data;
//using System.Data.SqlClient;

//public class EmployeeTableAdapter
//{
//    private string connectionString;

//    public EmployeeTableAdapter(string connectionString)
//    {
//        this.connectionString = connectionString;
//    }

//    // Загрузка всех сотрудников
//    public void Fill(EmployeesDataTable employees)
//    {
//        using (SqlConnection connection = new SqlConnection(connectionString))
//        {
//            connection.Open();
//            SqlCommand command = new SqlCommand("SELECT * FROM Employees", connection);
//            SqlDataAdapter adapter = new SqlDataAdapter(command);
//            adapter.Fill(employees);
//        }
//    }

//    // Обновление данных в базе
//    public void Update(EmployeesDataTable employees)
//    {
//        using (SqlConnection connection = new SqlConnection(connectionString))
//        {
//            connection.Open();
//            SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM Employees", connection);
//            SqlCommandBuilder builder = new SqlCommandBuilder(adapter);
//            adapter.UpdateCommand = builder.GetUpdateCommand();
//            adapter.InsertCommand = builder.GetInsertCommand();
//            adapter.DeleteCommand = builder.GetDeleteCommand();
//            adapter.Update(employees);
//        }
//    }

//    // Метод: Получить сотрудников по отделу
//    public EmployeesDataTable GetEmployeesByDepartment(string department)
//    {
//        EmployeesDataTable employees = new EmployeesDataTable();
//        using (SqlConnection connection = new SqlConnection(connectionString))
//        {
//            connection.Open();
//            SqlCommand command = new SqlCommand(
//                "SELECT * FROM Employees WHERE Department = @Department",
//                connection);
//            command.Parameters.AddWithValue("@Department", department);
//            SqlDataAdapter adapter = new SqlDataAdapter(command);
//            adapter.Fill(employees);
//        }
//        return employees;
//    }

//    // Метод: Получить сотрудников по датам найма
//    public EmployeesDataTable GetEmployeesByHireDate(DateTime fromDate, DateTime toDate)
//    {
//        EmployeesDataTable employees = new EmployeesDataTable();
//        using (SqlConnection connection = new SqlConnection(connectionString))
//        {
//            connection.Open();
//            SqlCommand command = new SqlCommand(
//                "SELECT * FROM Employees WHERE HireDate BETWEEN @FromDate AND @ToDate",
//                connection);
//            command.Parameters.AddWithValue("@FromDate", fromDate);
//            command.Parameters.AddWithValue("@ToDate", toDate);
//            SqlDataAdapter adapter = new SqlDataAdapter(command);
//            adapter.Fill(employees);
//        }
//        return employees;
//    }

//    // Метод: Получить топ сотрудников по зарплате
//    public EmployeesDataTable GetTopSalaryEmployees(int count)
//    {
//        EmployeesDataTable employees = new EmployeesDataTable();
//        using (SqlConnection connection = new SqlConnection(connectionString))
//        {
//            connection.Open();
//            SqlCommand command = new SqlCommand(
//                "SELECT TOP (@Count) * FROM Employees ORDER BY Salary DESC",
//                connection);
//            command.Parameters.AddWithValue("@Count", count);
//            SqlDataAdapter adapter = new SqlDataAdapter(command);
//            adapter.Fill(employees);
//        }
//        return employees;
//    }

//    // Метод: Получить количество сотрудников по отделам
//    public DataTable GetEmployeeCountByDepartment()
//    {
//        DataTable result = new DataTable();
//        using (SqlConnection connection = new SqlConnection(connectionString))
//        {
//            connection.Open();
//            SqlCommand command = new SqlCommand(
//                "SELECT Department, COUNT(*) AS Count FROM Employees GROUP BY Department",
//                connection);
//            SqlDataAdapter adapter = new SqlDataAdapter(command);
//            adapter.Fill(result);
//        }
//        return result;
//    }
//}


////Примеры методов использования
//using System;

//class Program
//{
//    static void Main()
//    {
//        string connectionString = "Your_Connection_String";
//        EmployeeTableAdapter adapter = new EmployeeTableAdapter(connectionString);

//        try
//        {
//            // Пример 1: Получить сотрудников отдела "IT"
//            EmployeesDataTable itEmployees = adapter.GetEmployeesByDepartment("IT");
//            Console.WriteLine("Сотрудники отдела IT:");
//            foreach (EmployeeRow emp in itEmployees.GetActiveEmployees())
//                Console.WriteLine(emp);

//            // Пример 2: Получить сотрудников, нанятых с 2020 по 2023 год
//            EmployeesDataTable hiredEmployees = adapter.GetEmployeesByHireDate(
//                new DateTime(2020, 1, 1),
//                new DateTime(2023, 12, 31));
//            Console.WriteLine("\nСотрудники, нанятые с 2020 по 2023:");
//            foreach (EmployeeRow emp in hiredEmployees.GetActiveEmployees())
//                Console.WriteLine(emp);

//            // Пример 3: Получить топ-3 сотрудников по зарплате
//            EmployeesDataTable topSalaryEmployees = adapter.GetTopSalaryEmployees(3);
//            Console.WriteLine("\nТоп-3 сотрудников по зарплате:");
//            foreach (EmployeeRow emp in topSalaryEmployees.GetActiveEmployees())
//                Console.WriteLine(emp);

//            // Пример 4: Получить количество сотрудников по отделам
//            DataTable departmentCounts = adapter.GetEmployeeCountByDepartment();
//            Console.WriteLine("\nКоличество сотрудников по отделам:");
//            foreach (DataRow row in departmentCounts.Rows)
//                Console.WriteLine($"{row["Department"]}: {row["Count"]}");
//        }
//        catch (Exception ex)
//        {
//            Console.WriteLine($"Ошибка: {ex.Message}");
//        }
//    }
//}


////20
//using System;
//using System.Data;
//using System.Windows.Forms;

//namespace EmployeeManagementApp
//{
//    public partial class MainForm : Form
//    {
//        private EmployeeDataSet employeeDataSet;
//        private EmployeeTableAdapter employeeTableAdapter;
//        private string connectionString = "Your_Connection_String";

//        public MainForm()
//        {
//            InitializeComponent();
//            InitializeDataComponents();
//            LoadData();
//        }

//        private void InitializeDataComponents()
//        {
//            employeeDataSet = new EmployeeDataSet();
//            employeeTableAdapter = new EmployeeTableAdapter(connectionString);
//            dataGridView1.DataSource = employeeDataSet.Employees;
//        }

//        private void LoadData()
//        {
//            try
//            {
//                employeeTableAdapter.Fill(employeeDataSet.Employees);
//                comboBoxDepartment.DataSource = employeeDataSet.Employees
//                    .AsEnumerable()
//                    .Select(row => row.Department)
//                    .Distinct()
//                    .ToList();
//            }
//            catch (Exception ex)
//            {
//                MessageBox.Show($"Ошибка загрузки данных: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
//            }
//        }

//        // Поиск сотрудников
//        private void btnSearch_Click(object sender, EventArgs e)
//        {
//            string searchText = textBoxSearch.Text.Trim();
//            if (!string.IsNullOrEmpty(searchText))
//            {
//                DataRow[] foundRows = employeeDataSet.Employees.Select($"Name LIKE '%{searchText}%'");
//                dataGridView1.DataSource = foundRows.Any() ? foundRows.CopyToDataTable() : null;
//            }
//            else
//            {
//                dataGridView1.DataSource = employeeDataSet.Employees;
//            }
//        }

//        // Фильтрация по отделу
//        private void comboBoxDepartment_SelectedIndexChanged(object sender, EventArgs e)
//        {
//            string selectedDepartment = comboBoxDepartment.SelectedValue.ToString();
//            if (!string.IsNullOrEmpty(selectedDepartment))
//            {
//                DataRow[] filteredRows = employeeDataSet.Employees.Select($"Department = '{selectedDepartment}'");
//                dataGridView1.DataSource = filteredRows.Any() ? filteredRows.CopyToDataTable() : null;
//            }
//        }

//        // Добавление новой записи
//        private void btnAdd_Click(object sender, EventArgs e)
//        {
//            EmployeeRow newEmployee = employeeDataSet.Employees.AddEmployeeRow(
//                "Новый сотрудник",
//                comboBoxDepartment.SelectedValue.ToString(),
//                0,
//                DateTime.Now,
//                "new@example.com",
//                "Active");
//            dataGridView1.Refresh();
//        }

//        // Редактирование выбранной записи
//        private void btnEdit_Click(object sender, EventArgs e)
//        {
//            if (dataGridView1.CurrentRow != null)
//            {
//                DataGridViewRow row = dataGridView1.CurrentRow;
//                EmployeeRow employee = employeeDataSet.Employees.FindByID((int)row.Cells["EmployeeID"].Value);
//                if (employee != null)
//                {
//                    employee.Name = "Измененное имя";
//                    employee.Salary = 50000;
//                }
//            }
//        }

//        // Удаление выбранной записи
//        private void btnDelete_Click(object sender, EventArgs e)
//        {
//            if (dataGridView1.CurrentRow != null)
//            {
//                DataGridViewRow row = dataGridView1.CurrentRow;
//                EmployeeRow employee = employeeDataSet.Employees.FindByID((int)row.Cells["EmployeeID"].Value);
//                if (employee != null)
//                {
//                    employee.Row.Delete();
//                }
//            }
//        }

//        // Сохранение изменений
//        private void btnSave_Click(object sender, EventArgs e)
//        {
//            try
//            {
//                employeeTableAdapter.Update(employeeDataSet.Employees);
//                employeeDataSet.AcceptChanges();
//                MessageBox.Show("Изменения сохранены!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
//            }
//            catch (Exception ex)
//            {
//                MessageBox.Show($"Ошибка сохранения: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
//                employeeDataSet.RejectChanges();
//            }
//        }
//    }
//}


////21
////EmployeeDataSet.cs
//using System;
//using System.Data;

//public partial class EmployeeDataSet : DataSet
//{
//    private EmployeesDataTable tableEmployees;
//    public EmployeesDataTable Employees => tableEmployees;

//    public EmployeeDataSet()
//    {
//        InitializeComponent();
//    }

//    private void InitializeComponent()
//    {
//        tableEmployees = new EmployeesDataTable("Employees");
//        this.Tables.Add(tableEmployees);
//    }
//}


////EmployeesDataTable.cs
//using System;
//using System.Data;

//public partial class EmployeesDataTable : DataTable
//{
//    public EmployeesDataTable() : base("Employees") { InitializeColumns(); }
//    public EmployeesDataTable(string tableName) : base(tableName) { InitializeColumns(); }

//    private void InitializeColumns()
//    {
//        DataColumn columnEmployeeID = new DataColumn("EmployeeID", typeof(int)) { AllowDBNull = false, AutoIncrement = true };
//        DataColumn columnName = new DataColumn("Name", typeof(string)) { AllowDBNull = false, MaxLength = 100 };
//        DataColumn columnDepartment = new DataColumn("Department", typeof(string)) { AllowDBNull = false, MaxLength = 50 };
//        DataColumn columnSalary = new DataColumn("Salary", typeof(decimal)) { AllowDBNull = false };
//        DataColumn columnHireDate = new DataColumn("HireDate", typeof(DateTime)) { AllowDBNull = false };
//        DataColumn columnEmail = new DataColumn("Email", typeof(string)) { AllowDBNull = false, MaxLength = 100 };
//        DataColumn columnStatus = new DataColumn("Status", typeof(string)) { AllowDBNull = false, DefaultValue = "Active" };

//        this.Columns.AddRange(new DataColumn[] { columnEmployeeID, columnName, columnDepartment, columnSalary, columnHireDate, columnEmail, columnStatus });
//        this.PrimaryKey = new DataColumn[] { columnEmployeeID };
//    }

//    public EmployeeRow AddEmployeeRow(string name, string department, decimal salary, DateTime hireDate, string email, string status)
//    {
//        EmployeeRow row = new EmployeeRow(this.NewRow());
//        row.Name = name;
//        row.Department = department;
//        row.Salary = salary;
//        row.HireDate = hireDate;
//        row.Email = email;
//        row.Status = status;
//        this.Rows.Add(row.Row);
//        return row;
//    }

//    public EmployeeRow FindByID(int employeeID)
//    {
//        DataRow row = this.Rows.Find(employeeID);
//        return row != null ? new EmployeeRow(row) : null;
//    }
//}


////EmployeeRow.cs
//using System;
//using System.Data;

//public class EmployeeRow
//{
//    private DataRow row;
//    public EmployeeRow(DataRow row) { this.row = row; }

//    public int EmployeeID { get => (int)row["EmployeeID"]; set => row["EmployeeID"] = value; }
//    public string Name { get => (string)row["Name"]; set => row["Name"] = value; }
//    public string Department { get => (string)row["Department"]; set => row["Department"] = value; }
//    public decimal Salary { get => (decimal)row["Salary"]; set => row["Salary"] = value; }
//    public DateTime HireDate { get => (DateTime)row["HireDate"]; set => row["HireDate"] = value; }
//    public string Email { get => (string)row["Email"]; set => row["Email"] = value; }
//    public string Status { get => (string)row["Status"]; set => row["Status"] = value; }
//    public DataRow Row => row;
//}


////EmployeeTableAdapter.cs
//using System;
//using System.Data;
//using System.Data.SqlClient;

//public class EmployeeTableAdapter
//{
//    private string connectionString;

//    public EmployeeTableAdapter(string connectionString)
//    {
//        this.connectionString = connectionString;
//    }

//    public void Fill(EmployeesDataTable employees)
//    {
//        using (SqlConnection connection = new SqlConnection(connectionString))
//        {
//            connection.Open();
//            SqlCommand command = new SqlCommand("SELECT * FROM Employees", connection);
//            SqlDataAdapter adapter = new SqlDataAdapter(command);
//            adapter.Fill(employees);
//        }
//    }

//    public void Update(EmployeesDataTable employees)
//    {
//        using (SqlConnection connection = new SqlConnection(connectionString))
//        {
//            connection.Open();
//            SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM Employees", connection);
//            SqlCommandBuilder builder = new SqlCommandBuilder(adapter);
//            adapter.UpdateCommand = builder.GetUpdateCommand();
//            adapter.InsertCommand = builder.GetInsertCommand();
//            adapter.DeleteCommand = builder.GetDeleteCommand();
//            adapter.Update(employees);
//        }
//    }
//}


////EmployeeService.cs
//using System;
//using System.Data;

//public class EmployeeService
//{
//    private EmployeeTableAdapter employeeTableAdapter;

//    public EmployeeService(string connectionString)
//    {
//        employeeTableAdapter = new EmployeeTableAdapter(connectionString);
//    }

//    public EmployeesDataTable GetAllEmployees()
//    {
//        EmployeesDataTable employees = new EmployeesDataTable();
//        employeeTableAdapter.Fill(employees);
//        return employees;
//    }

//    public void SaveEmployees(EmployeesDataTable employees)
//    {
//        employeeTableAdapter.Update(employees);
//    }

//    public void ValidateEmployee(EmployeeRow employee)
//    {
//        if (employee.Salary < 0)
//            throw new ArgumentException("Зарплата не может быть отрицательной.");
//        if (string.IsNullOrEmpty(employee.Email) || !employee.Email.Contains("@"))
//            throw new ArgumentException("Некорректный email.");
//    }
//}


////MainForm.cs
//using System;
//using System.Windows.Forms;

//namespace EmployeeManagementApp
//{
//    public partial class MainForm : Form
//    {
//        private EmployeeService employeeService;
//        private EmployeeDataSet employeeDataSet;

//        public MainForm()
//        {
//            InitializeComponent();
//            string connectionString = "Your_Connection_String";
//            employeeService = new EmployeeService(connectionString);
//            employeeDataSet = new EmployeeDataSet();
//            LoadData();
//        }

//        private void LoadData()
//        {
//            try
//            {
//                employeeDataSet.Employees.Clear();
//                EmployeesDataTable employees = employeeService.GetAllEmployees();
//                foreach (EmployeeRow employee in employees)
//                    employeeDataSet.Employees.ImportRow(employee.Row);
//                dataGridView1.DataSource = employeeDataSet.Employees;
//            }
//            catch (Exception ex)
//            {
//                MessageBox.Show($"Ошибка загрузки данных: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
//            }
//        }

//        private void btnAdd_Click(object sender, EventArgs e)
//        {
//            try
//            {
//                EmployeeRow newEmployee = employeeDataSet.Employees.AddEmployeeRow(
//                    "Новый сотрудник",
//                    "IT",
//                    50000,
//                    DateTime.Now,
//                    "new@example.com",
//                    "Active");
//                dataGridView1.Refresh();
//            }
//            catch (Exception ex)
//            {
//                MessageBox.Show($"Ошибка добавления: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
//            }
//        }

//        private void btnEdit_Click(object sender, EventArgs e)
//        {
//            if (dataGridView1.CurrentRow != null)
//            {
//                try
//                {
//                    DataGridViewRow row = dataGridView1.CurrentRow;
//                    EmployeeRow employee = employeeDataSet.Employees.FindByID((int)row.Cells["EmployeeID"].Value);
//                    if (employee != null)
//                    {
//                        employee.Name = "Измененное имя";
//                        employee.Salary = 60000;
//                        employeeService.ValidateEmployee(employee);
//                    }
//                }
//                catch (Exception ex)
//                {
//                    MessageBox.Show($"Ошибка редактирования: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
//                }
//            }
//        }

//        private void btnDelete_Click(object sender, EventArgs e)
//        {
//            if (dataGridView1.CurrentRow != null)
//            {
//                try
//                {
//                    DataGridViewRow row = dataGridView1.CurrentRow;
//                    EmployeeRow employee = employeeDataSet.Employees.FindByID((int)row.Cells["EmployeeID"].Value);
//                    if (employee != null)
//                        employee.Row.Delete();
//                }
//                catch (Exception ex)
//                {
//                    MessageBox.Show($"Ошибка удаления: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
//                }
//            }
//        }

//        private void btnSave_Click(object sender, EventArgs e)
//        {
//            try
//            {
//                employeeService.SaveEmployees(employeeDataSet.Employees);
//                MessageBox.Show("Изменения сохранены!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
//            }
//            catch (Exception ex)
//            {
//                MessageBox.Show($"Ошибка сохранения: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
//            }
//        }
//    }
//}


////22
////OrderDataSet.cs
//using System;
//using System.Data;

//public partial class OrderDataSet : DataSet
//{
//    private OrdersDataTable tableOrders;
//    private CustomersDataTable tableCustomers;

//    public OrdersDataTable Orders => tableOrders;
//    public CustomersDataTable Customers => tableCustomers;

//    public OrderDataSet()
//    {
//        InitializeComponent();
//    }

//    private void InitializeComponent()
//    {
//        tableOrders = new OrdersDataTable("Orders");
//        tableCustomers = new CustomersDataTable("Customers");
//        this.Tables.Add(tableOrders);
//        this.Tables.Add(tableCustomers);
//    }
//}


////OrdersDataTable.cs
//using System;
//using System.Data;

//public partial class OrdersDataTable : DataTable
//{
//    public OrdersDataTable() : base("Orders") { InitializeColumns(); }
//    public OrdersDataTable(string tableName) : base(tableName) { InitializeColumns(); }

//    private void InitializeColumns()
//    {
//        DataColumn columnOrderID = new DataColumn("OrderID", typeof(int)) { AllowDBNull = false, AutoIncrement = true };
//        DataColumn columnCustomerID = new DataColumn("CustomerID", typeof(int)) { AllowDBNull = false };
//        DataColumn columnAmount = new DataColumn("Amount", typeof(decimal)) { AllowDBNull = false };
//        DataColumn columnOrderDate = new DataColumn("OrderDate", typeof(DateTime)) { AllowDBNull = false };

//        this.Columns.AddRange(new DataColumn[] { columnOrderID, columnCustomerID, columnAmount, columnOrderDate });
//        this.PrimaryKey = new DataColumn[] { columnOrderID };
//    }
//}


////CustomersDataTable.cs
//using System;
//using System.Data;

//public partial class CustomersDataTable : DataTable
//{
//    public CustomersDataTable() : base("Customers") { InitializeColumns(); }
//    public CustomersDataTable(string tableName) : base(tableName) { InitializeColumns(); }

//    private void InitializeColumns()
//    {
//        DataColumn columnCustomerID = new DataColumn("CustomerID", typeof(int)) { AllowDBNull = false, AutoIncrement = true };
//        DataColumn columnName = new DataColumn("Name", typeof(string)) { AllowDBNull = false, MaxLength = 100 };

//        this.Columns.AddRange(new DataColumn[] { columnCustomerID, columnName });
//        this.PrimaryKey = new DataColumn[] { columnCustomerID };
//    }
//}


////OrderService.cs
//using System;
//using System.Data;
//using System.Linq;

//public class OrderService
//{
//    private OrderDataSet orderDataSet;

//    public OrderService()
//    {
//        orderDataSet = new OrderDataSet();
//    }

//    public DataTable GetTotalOrdersByCustomer()
//    {
//        var query = from order in orderDataSet.Orders.AsEnumerable()
//                    join customer in orderDataSet.Customers.AsEnumerable()
//                    on order.Field<int>("CustomerID") equals customer.Field<int>("CustomerID")
//                    group order by customer.Field<string>("Name") into g
//                    select new
//                    {
//                        CustomerName = g.Key,
//                        TotalOrders = g.Count(),
//                        TotalAmount = g.Sum(o => o.Field<decimal>("Amount"))
//                    };

//        DataTable result = new DataTable();
//        result.Columns.Add("CustomerName", typeof(string));
//        result.Columns.Add("TotalOrders", typeof(int));
//        result.Columns.Add("TotalAmount", typeof(decimal));

//        foreach (var item in query)
//        {
//            result.Rows.Add(item.CustomerName, item.TotalOrders, item.TotalAmount);
//        }

//        return result;
//    }

//    public decimal GetAverageOrderValue()
//    {
//        return orderDataSet.Orders.AsEnumerable().Average(o => o.Field<decimal>("Amount"));
//    }

//    public DataTable GetTopCustomers(int count)
//    {
//        var query = from order in orderDataSet.Orders.AsEnumerable()
//                    join customer in orderDataSet.Customers.AsEnumerable()
//                    on order.Field<int>("CustomerID") equals customer.Field<int>("CustomerID")
//                    group order by customer.Field<string>("Name") into g
//                    orderby g.Sum(o => o.Field<decimal>("Amount")) descending
//                    select new
//                    {
//                        CustomerName = g.Key,
//                        TotalAmount = g.Sum(o => o.Field<decimal>("Amount"))
//                    };

//        DataTable result = new DataTable();
//        result.Columns.Add("CustomerName", typeof(string));
//        result.Columns.Add("TotalAmount", typeof(decimal));

//        foreach (var item in query.Take(count))
//        {
//            result.Rows.Add(item.CustomerName, item.TotalAmount);
//        }

//        return result;
//    }

//    public DataTable GetOrdersByDateRange(DateTime fromDate, DateTime toDate)
//    {
//        var query = from order in orderDataSet.Orders.AsEnumerable()
//                    where order.Field<DateTime>("OrderDate") >= fromDate && order.Field<DateTime>("OrderDate") <= toDate
//                    select order;

//        DataTable result = query.Any() ? query.CopyToDataTable() : new DataTable();
//        return result;
//    }
//}


////Programm.cs
//using System;
//using System.Data;

//class Program
//{
//    static void Main()
//    {
//        OrderService orderService = new OrderService();

//        // Пример 1: Общее количество заказов по клиентам
//        DataTable totalOrdersByCustomer = orderService.GetTotalOrdersByCustomer();
//        Console.WriteLine("Общее количество заказов по клиентам:");
//        foreach (DataRow row in totalOrdersByCustomer.Rows)
//            Console.WriteLine($"{row["CustomerName"]}: {row["TotalOrders"]} заказов на сумму {row["TotalAmount"]:C}");

//        // Пример 2: Средняя стоимость заказа
//        decimal averageOrderValue = orderService.GetAverageOrderValue();
//        Console.WriteLine($"\nСредняя стоимость заказа: {averageOrderValue:C}");

//        // Пример 3: Топ-3 клиентов по сумме заказов
//        DataTable topCustomers = orderService.GetTopCustomers(3);
//        Console.WriteLine("\nТоп-3 клиентов по сумме заказов:");
//        foreach (DataRow row in topCustomers.Rows)
//            Console.WriteLine($"{row["CustomerName"]}: {row["TotalAmount"]:C}");

//        // Пример 4: Заказы за последний месяц
//        DataTable ordersLastMonth = orderService.GetOrdersByDateRange(
//            DateTime.Now.AddMonths(-1),
//            DateTime.Now);
//        Console.WriteLine("\nЗаказы за последний месяц:");
//        foreach (DataRow row in ordersLastMonth.Rows)
//            Console.WriteLine($"ID заказа: {row["OrderID"]}, Сумма: {row["Amount"]:C}, Дата: {row["OrderDate"]:d}");
//    }
//}


////23
////CompanyDataSet.cs
//using System;
//using System.Data;

//public partial class CompanyDataSet : DataSet
//{
//    private CompaniesDataTable tableCompanies;
//    private DepartmentsDataTable tableDepartments;
//    private EmployeesDataTable tableEmployees;

//    public CompaniesDataTable Companies => tableCompanies;
//    public DepartmentsDataTable Departments => tableDepartments;
//    public EmployeesDataTable Employees => tableEmployees;

//    public CompanyDataSet()
//    {
//        InitializeComponent();
//    }

//    private void InitializeComponent()
//    {
//        tableCompanies = new CompaniesDataTable("Companies");
//        tableDepartments = new DepartmentsDataTable("Departments");
//        tableEmployees = new EmployeesDataTable("Employees");

//        this.Tables.Add(tableCompanies);
//        this.Tables.Add(tableDepartments);
//        this.Tables.Add(tableEmployees);

//        // Создание отношений
//        DataRelation companyDepartmentRelation = new DataRelation(
//            "CompanyDepartments",
//            tableCompanies.Columns["CompanyID"],
//            tableDepartments.Columns["CompanyID"]);
//        this.Relations.Add(companyDepartmentRelation);

//        DataRelation departmentEmployeeRelation = new DataRelation(
//            "DepartmentEmployees",
//            tableDepartments.Columns["DepartmentID"],
//            tableEmployees.Columns["DepartmentID"]);
//        this.Relations.Add(departmentEmployeeRelation);
//    }
//}


////CompaniesDataTable.cs
//using System;
//using System.Data;

//public partial class CompaniesDataTable : DataTable
//{
//    public CompaniesDataTable() : base("Companies") { InitializeColumns(); }
//    public CompaniesDataTable(string tableName) : base(tableName) { InitializeColumns(); }

//    private void InitializeColumns()
//    {
//        DataColumn columnCompanyID = new DataColumn("CompanyID", typeof(int)) { AllowDBNull = false, AutoIncrement = true };
//        DataColumn columnName = new DataColumn("Name", typeof(string)) { AllowDBNull = false, MaxLength = 100 };

//        this.Columns.AddRange(new DataColumn[] { columnCompanyID, columnName });
//        this.PrimaryKey = new DataColumn[] { columnCompanyID };
//    }

//    public CompanyRow AddCompanyRow(string name)
//    {
//        CompanyRow row = new CompanyRow(this.NewRow());
//        row.Name = name;
//        this.Rows.Add(row.Row);
//        return row;
//    }

//    public CompanyRow FindByID(int companyID)
//    {
//        DataRow dataRow = this.Rows.Find(companyID);
//        return dataRow != null ? new CompanyRow(dataRow) : null;
//    }
//}


////DepartmentsDataTable.cs
//using System;
//using System.Data;

//public partial class DepartmentsDataTable : DataTable
//{
//    public DepartmentsDataTable() : base("Departments") { InitializeColumns(); }
//    public DepartmentsDataTable(string tableName) : base(tableName) { InitializeColumns(); }

//    private void InitializeColumns()
//    {
//        DataColumn columnDepartmentID = new DataColumn("DepartmentID", typeof(int)) { AllowDBNull = false, AutoIncrement = true };
//        DataColumn columnCompanyID = new DataColumn("CompanyID", typeof(int)) { AllowDBNull = false };
//        DataColumn columnName = new DataColumn("Name", typeof(string)) { AllowDBNull = false, MaxLength = 100 };

//        this.Columns.AddRange(new DataColumn[] { columnDepartmentID, columnCompanyID, columnName });
//        this.PrimaryKey = new DataColumn[] { columnDepartmentID };
//    }

//    public DepartmentRow AddDepartmentRow(int companyID, string name)
//    {
//        DepartmentRow row = new DepartmentRow(this.NewRow());
//        row.CompanyID = companyID;
//        row.Name = name;
//        this.Rows.Add(row.Row);
//        return row;
//    }

//    public DepartmentRow[] GetDepartmentsByCompany(int companyID)
//    {
//        DataRow[] rows = this.Select($"CompanyID = {companyID}");
//        DepartmentRow[] result = new DepartmentRow[rows.Length];
//        for (int i = 0; i < rows.Length; i++)
//            result[i] = new DepartmentRow(rows[i]);
//        return result;
//    }
//}


////EmployeesDataTable.cs
//using System;
//using System.Data;

//public partial class EmployeesDataTable : DataTable
//{
//    public EmployeesDataTable() : base("Employees") { InitializeColumns(); }
//    public EmployeesDataTable(string tableName) : base(tableName) { InitializeColumns(); }

//    private void InitializeColumns()
//    {
//        DataColumn columnEmployeeID = new DataColumn("EmployeeID", typeof(int)) { AllowDBNull = false, AutoIncrement = true };
//        DataColumn columnDepartmentID = new DataColumn("DepartmentID", typeof(int)) { AllowDBNull = false };
//        DataColumn columnName = new DataColumn("Name", typeof(string)) { AllowDBNull = false, MaxLength = 100 };
//        DataColumn columnSalary = new DataColumn("Salary", typeof(decimal)) { AllowDBNull = false };

//        this.Columns.AddRange(new DataColumn[] { columnEmployeeID, columnDepartmentID, columnName, columnSalary });
//        this.PrimaryKey = new DataColumn[] { columnEmployeeID };
//    }

//    public EmployeeRow AddEmployeeRow(int departmentID, string name, decimal salary)
//    {
//        EmployeeRow row = new EmployeeRow(this.NewRow());
//        row.DepartmentID = departmentID;
//        row.Name = name;
//        row.Salary = salary;
//        this.Rows.Add(row.Row);
//        return row;
//    }

//    public EmployeeRow[] GetEmployeesByDepartment(int departmentID)
//    {
//        DataRow[] rows = this.Select($"DepartmentID = {departmentID}");
//        EmployeeRow[] result = new EmployeeRow[rows.Length];
//        for (int i = 0; i < rows.Length; i++)
//            result[i] = new EmployeeRow(rows[i]);
//        return result;
//    }
//}


////Классы строк (CompanyRow, DepartmentRow,СотрудникRow)
//public class CompanyRow
//{
//    private DataRow row;
//    public CompanyRow(DataRow row) { this.row = row; }
//    public int CompanyID { get => (int)row["CompanyID"]; set => row["CompanyID"] = value; }
//    public string Name { get => (string)row["Name"]; set => row["Name"] = value; }
//    public DataRow Row => row;
//}

//public class DepartmentRow
//{
//    private DataRow row;
//    public DepartmentRow(DataRow row) { this.row = row; }
//    public int DepartmentID { get => (int)row["DepartmentID"]; set => row["DepartmentID"] = value; }
//    public int CompanyID { get => (int)row["CompanyID"]; set => row["CompanyID"] = value; }
//    public string Name { get => (string)row["Name"]; set => row["Name"] = value; }
//    public DataRow Row => row;
//}

//public class EmployeeRow
//{
//    private DataRow row;
//    public EmployeeRow(DataRow row) { this.row = row; }
//    public int EmployeeID { get => (int)row["EmployeeID"]; set => row["EmployeeID"] = value; }
//    public int DepartmentID { get => (int)row["DepartmentID"]; set => row["DepartmentID"] = value; }
//    public string Name { get => (string)row["Name"]; set => row["Name"] = value; }
//    public decimal Salary { get => (decimal)row["Salary"]; set => row["Salary"] = value; }
//    public DataRow Row => row;
//}


////CompanyService.cs
//using System;
//using System.Data;

//public class CompanyService
//{
//    private CompanyDataSet companyDataSet;

//    public CompanyService()
//    {
//        companyDataSet = new CompanyDataSet();
//    }

//    public DepartmentRow[] GetDepartmentsByCompany(int companyID)
//    {
//        CompanyRow company = companyDataSet.Companies.FindByID(companyID);
//        if (company == null)
//            throw new ArgumentException("Компания не найдена.");

//        return companyDataSet.Departments.GetDepartmentsByCompany(companyID);
//    }

//    public EmployeeRow[] GetEmployeesByDepartment(int departmentID)
//    {
//        DepartmentRow department = companyDataSet.Departments.FindByID(departmentID);
//        if (department == null)
//            throw new ArgumentException("Отдел не найден.");

//        return companyDataSet.Employees.GetEmployeesByDepartment(departmentID);
//    }

//    public DataTable GetCompanyHierarchy(int companyID)
//    {
//        DataTable hierarchy = new DataTable();
//        hierarchy.Columns.Add("CompanyName", typeof(string));
//        hierarchy.Columns.Add("DepartmentName", typeof(string));
//        hierarchy.Columns.Add("EmployeeName", typeof(string));
//        hierarchy.Columns.Add("Salary", typeof(decimal));

//        CompanyRow company = companyDataSet.Companies.FindByID(companyID);
//        if (company == null)
//            throw new ArgumentException("Компания не найдена.");

//        foreach (DepartmentRow department in GetDepartmentsByCompany(companyID))
//        {
//            foreach (EmployeeRow employee in GetEmployeesByDepartment(department.DepartmentID))
//            {
//                hierarchy.Rows.Add(
//                    company.Name,
//                    department.Name,
//                    employee.Name,
//                    employee.Salary);
//            }
//        }

//        return hierarchy;
//    }

//    public decimal GetTotalSalaryByDepartment(int departmentID)
//    {
//        EmployeeRow[] employees = companyDataSet.Employees.GetEmployeesByDepartment(departmentID);
//        return employees.Sum(e => e.Salary);
//    }

//    public decimal GetTotalSalaryByCompany(int companyID)
//    {
//        DepartmentRow[] departments = GetDepartmentsByCompany(companyID);
//        decimal totalSalary = 0;
//        foreach (DepartmentRow department in departments)
//            totalSalary += GetTotalSalaryByDepartment(department.DepartmentID);
//        return totalSalary;
//    }
//}


////Programm.cs
//using System;
//using System.Data;

//class Program
//{
//    static void Main()
//    {
//        CompanyService companyService = new CompanyService();

//        // Пример 1: Получение департаментов по компании
//        try
//        {
//            DepartmentRow[] departments = companyService.GetDepartmentsByCompany(1);
//            Console.WriteLine("Департаменты компании:");
//            foreach (DepartmentRow department in departments)
//                Console.WriteLine($"{department.DepartmentID}: {department.Name}");
//        }
//        catch (Exception ex)
//        {
//            Console.WriteLine($"Ошибка: {ex.Message}");
//        }

//        // Пример 2: Получение сотрудников по департаменту
//        try
//        {
//            EmployeeRow[] employees = companyService.GetEmployeesByDepartment(1);
//            Console.WriteLine("\nСотрудники департамента:");
//            foreach (EmployeeRow employee in employees)
//                Console.WriteLine($"{employee.EmployeeID}: {employee.Name}, Зарплата: {employee.Salary:C}");
//        }
//        catch (Exception ex)
//        {
//            Console.WriteLine($"Ошибка: {ex.Message}");
//        }

//        // Пример 3: Получение иерархии компании
//        try
//        {
//            DataTable hierarchy = companyService.GetCompanyHierarchy(1);
//            Console.WriteLine("\nИерархия компании:");
//            foreach (DataRow row in hierarchy.Rows)
//                Console.WriteLine($"{row["CompanyName"]} -> {row["DepartmentName"]} -> {row["EmployeeName"]} ({row["Salary"]:C})");
//        }
//        catch (Exception ex)
//        {
//            Console.WriteLine($"Ошибка: {ex.Message}");
//        }

//        // Пример 4: Агрегация зарплат по департаменту
//        try
//        {
//            decimal totalSalary = companyService.GetTotalSalaryByDepartment(1);
//            Console.WriteLine($"\nОбщая зарплата по департаменту: {totalSalary:C}");
//        }
//        catch (Exception ex)
//        {
//            Console.WriteLine($"Ошибка: {ex.Message}");
//        }

//        // Пример 5: Агрегация зарплат по компании
//        try
//        {
//            decimal totalSalary = companyService.GetTotalSalaryByCompany(1);
//            Console.WriteLine($"\nОбщая зарплата по компании: {totalSalary:C}");
//        }
//        catch (Exception ex)
//        {
//            Console.WriteLine($"Ошибка: {ex.Message}");
//        }
//    }
//}


////24
////OrderDataSet.cs
//using System;
//using System.Data;

//public partial class OrderDataSet : DataSet
//{
//    private OrdersDataTable tableOrders;

//    public OrdersDataTable Orders => tableOrders;

//    public OrderDataSet()
//    {
//        InitializeComponent();
//    }

//    private void InitializeComponent()
//    {
//        tableOrders = new OrdersDataTable("Orders");
//        this.Tables.Add(tableOrders);
//    }
//}


////OrdersDataTable.cs
//using System;
//using System.Data;

//public partial class OrdersDataTable : DataTable
//{
//    public OrdersDataTable() : base("Orders") { InitializeColumns(); }
//    public OrdersDataTable(string tableName) : base(tableName) { InitializeColumns(); }

//    private void InitializeColumns()
//    {
//        DataColumn columnOrderID = new DataColumn("OrderID", typeof(int)) { AllowDBNull = false, AutoIncrement = true };
//        DataColumn columnQuantity = new DataColumn("Quantity", typeof(int)) { AllowDBNull = false };
//        DataColumn columnPrice = new DataColumn("Price", typeof(decimal)) { AllowDBNull = false };

//        // Вычисляемые поля
//        DataColumn columnTotal = new DataColumn("Total", typeof(decimal), "Quantity * Price");
//        DataColumn columnTax = new DataColumn("Tax", typeof(decimal), "Total * 0.18");
//        DataColumn columnTotalWithTax = new DataColumn("TotalWithTax", typeof(decimal), "Total + Tax");

//        this.Columns.AddRange(new DataColumn[] { columnOrderID, columnQuantity, columnPrice, columnTotal, columnTax, columnTotalWithTax });
//        this.PrimaryKey = new DataColumn[] { columnOrderID };
//    }

//    public OrderRow AddOrderRow(int quantity, decimal price)
//    {
//        OrderRow row = new OrderRow(this.NewRow());
//        row.Quantity = quantity;
//        row.Price = price;
//        this.Rows.Add(row.Row);
//        return row;
//    }
//}


////OrderRow.cs
//using System;
//using System.Data;

//public class OrderRow
//{
//    private DataRow row;
//    public OrderRow(DataRow row) { this.row = row; }

//    public int OrderID { get => (int)row["OrderID"]; set => row["OrderID"] = value; }
//    public int Quantity { get => (int)row["Quantity"]; set => row["Quantity"] = value; }
//    public decimal Price { get => (decimal)row["Price"]; set => row["Price"] = value; }
//    public decimal Total => (decimal)row["Total"];
//    public decimal Tax => (decimal)row["Tax"];
//    public decimal TotalWithTax => (decimal)row["TotalWithTax"];
//    public DataRow Row => row;
//}


////OrderService.cs
//using System;
//using System.Data;

//public class OrderService
//{
//    private OrderDataSet orderDataSet;

//    public OrderService()
//    {
//        orderDataSet = new OrderDataSet();
//    }

//    public void AddOrder(int quantity, decimal price)
//    {
//        orderDataSet.Orders.AddOrderRow(quantity, price);
//    }

//    public DataTable GetAllOrders()
//    {
//        return orderDataSet.Orders;
//    }

//    public decimal GetTotalRevenue()
//    {
//        return orderDataSet.Orders.AsEnumerable().Sum(row => row.Field<decimal>("TotalWithTax"));
//    }

//    public decimal GetTotalTax()
//    {
//        return orderDataSet.Orders.AsEnumerable().Sum(row => row.Field<decimal>("Tax"));
//    }
//}


////Programm.cs
//using System;
//using System.Data;

//class Program
//{
//    static void Main()
//    {
//        OrderService orderService = new OrderService();

//        // Пример 1: Добавление заказа
//        orderService.AddOrder(5, 100);
//        orderService.AddOrder(3, 200);

//        // Пример 2: Отображение всех заказов с вычисляемыми полями
//        DataTable orders = orderService.GetAllOrders();
//        Console.WriteLine("Список заказов:");
//        foreach (DataRow row in orders.Rows)
//            Console.WriteLine($"ID: {row["OrderID"]}, Количество: {row["Quantity"]}, Цена: {row["Price"]:C}, Итого: {row["Total"]:C}, Налог: {row["Tax"]:C}, Итого с налогом: {row["TotalWithTax"]:C}");

//        // Пример 3: Получение общей выручки
//        decimal totalRevenue = orderService.GetTotalRevenue();
//        Console.WriteLine($"\nОбщая выручка: {totalRevenue:C}");

//        // Пример 4: Получение общей суммы налогов
//        decimal totalTax = orderService.GetTotalTax();
//        Console.WriteLine($"\nОбщая сумма налогов: {totalTax:C}");
//    }
//}


////25
////EmployeeDataSet.cs
//using System;
//using System.Data;

//public partial class EmployeeDataSet : DataSet
//{
//    private EmployeesDataTable tableEmployees;
//    private DepartmentsDataTable tableDepartments;

//    public EmployeesDataTable Employees => tableEmployees;
//    public DepartmentsDataTable Departments => tableDepartments;

//    public EmployeeDataSet()
//    {
//        InitializeComponent();
//    }

//    private void InitializeComponent()
//    {
//        tableDepartments = new DepartmentsDataTable("Departments");
//        tableEmployees = new EmployeesDataTable("Employees");

//        this.Tables.Add(tableDepartments);
//        this.Tables.Add(tableEmployees);

//        // Создание отношения с внешним ключом
//        DataRelation departmentEmployeeRelation = new DataRelation(
//            "DepartmentEmployees",
//            tableDepartments.Columns["DepartmentID"],
//            tableEmployees.Columns["DepartmentID"],
//            false); // Не обновлять дочерние записи при изменении родительского ключа
//        this.Relations.Add(departmentEmployeeRelation);
//    }
//}


////DepartmentsDataTable.cs
//using System;
//using System.Data;

//public partial class DepartmentsDataTable : DataTable
//{
//    public DepartmentsDataTable() : base("Departments") { InitializeColumns(); }
//    public DepartmentsDataTable(string tableName) : base(tableName) { InitializeColumns(); }

//    private void InitializeColumns()
//    {
//        DataColumn columnDepartmentID = new DataColumn("DepartmentID", typeof(int))
//        {
//            AllowDBNull = false,
//            AutoIncrement = true,
//            Unique = true // Уникальное ограничение
//        };

//        DataColumn columnName = new DataColumn("Name", typeof(string))
//        {
//            AllowDBNull = false,
//            MaxLength = 100
//        };

//        this.Columns.AddRange(new DataColumn[] { columnDepartmentID, columnName });
//        this.PrimaryKey = new DataColumn[] { columnDepartmentID }; // Первичный ключ
//    }

//    public DepartmentRow AddDepartmentRow(string name)
//    {
//        DepartmentRow row = new DepartmentRow(this.NewRow());
//        row.Name = name;
//        this.Rows.Add(row.Row);
//        return row;
//    }

//    public DepartmentRow FindByID(int departmentID)
//    {
//        DataRow dataRow = this.Rows.Find(departmentID);
//        return dataRow != null ? new DepartmentRow(dataRow) : null;
//    }
//}


////EmployeesDataTable.cs
//using System;
//using System.Data;

//public partial class EmployeesDataTable : DataTable
//{
//    public EmployeesDataTable() : base("Employees") { InitializeColumns(); }
//    public EmployeesDataTable(string tableName) : base(tableName) { InitializeColumns(); }

//    private void InitializeColumns()
//    {
//        DataColumn columnEmployeeID = new DataColumn("EmployeeID", typeof(int))
//        {
//            AllowDBNull = false,
//            AutoIncrement = true,
//            Unique = true // Уникальное ограничение
//        };

//        DataColumn columnDepartmentID = new DataColumn("DepartmentID", typeof(int))
//        {
//            AllowDBNull = false // Внешний ключ, не может быть NULL
//        };

//        DataColumn columnName = new DataColumn("Name", typeof(string))
//        {
//            AllowDBNull = false,
//            MaxLength = 100
//        };

//        DataColumn columnSalary = new DataColumn("Salary", typeof(decimal))
//        {
//            AllowDBNull = false,
//            DefaultValue = 0
//        };

//        this.Columns.AddRange(new DataColumn[] { columnEmployeeID, columnDepartmentID, columnName, columnSalary });
//        this.PrimaryKey = new DataColumn[] { columnEmployeeID }; // Первичный ключ

//        // Ограничение: Зарплата не может быть отрицательной
//        this.ColumnChanging += (sender, e) =>
//        {
//            if (e.Column.ColumnName == "Salary" && (decimal)e.ProposedValue < 0)
//                throw new ArgumentException("Зарплата не может быть отрицательной.");
//        };
//    }

//    public EmployeeRow AddEmployeeRow(int departmentID, string name, decimal salary)
//    {
//        EmployeeRow row = new EmployeeRow(this.NewRow());
//        row.DepartmentID = departmentID;
//        row.Name = name;
//        row.Salary = salary;
//        this.Rows.Add(row.Row);
//        return row;
//    }

//    public EmployeeRow FindByID(int employeeID)
//    {
//        DataRow dataRow = this.Rows.Find(employeeID);
//        return dataRow != null ? new EmployeeRow(dataRow) : null;
//    }
//}


////Классы строк (DepartmentRow,СотрудникRow)
//public class DepartmentRow
//{
//    private DataRow row;
//    public DepartmentRow(DataRow row) { this.row = row; }
//    public int DepartmentID { get => (int)row["DepartmentID"]; set => row["DepartmentID"] = value; }
//    public string Name { get => (string)row["Name"]; set => row["Name"] = value; }
//    public DataRow Row => row;
//}

//public class EmployeeRow
//{
//    private DataRow row;
//    public EmployeeRow(DataRow row) { this.row = row; }
//    public int EmployeeID { get => (int)row["EmployeeID"]; set => row["EmployeeID"] = value; }
//    public int DepartmentID { get => (int)row["DepartmentID"]; set => row["DepartmentID"] = value; }
//    public string Name { get => (string)row["Name"]; set => row["Name"] = value; }
//    public decimal Salary { get => (decimal)row["Salary"]; set => row["Salary"] = value; }
//    public DataRow Row => row;
//}


////EmployeeService.cs
//using System;
//using System.Data;

//public class EmployeeService
//{
//    private EmployeeDataSet employeeDataSet;

//    public EmployeeService()
//    {
//        employeeDataSet = new EmployeeDataSet();
//    }

//    public void AddDepartment(string name)
//    {
//        employeeDataSet.Departments.AddDepartmentRow(name);
//    }

//    public void AddEmployee(int departmentID, string name, decimal salary)
//    {
//        // Проверка существования отдела
//        if (employeeDataSet.Departments.FindByID(departmentID) == null)
//            throw new ArgumentException("Отдел не найден.");

//        employeeDataSet.Employees.AddEmployeeRow(departmentID, name, salary);
//    }

//    public void ValidateEmployee(EmployeeRow employee)
//    {
//        if (employee.Salary < 0)
//            throw new ArgumentException("Зарплата не может быть отрицательной.");
//    }

//    public void PrintConstraintsReport()
//    {
//        Console.WriteLine("Отчёт об ограничениях:");
//        Console.WriteLine("1. Первичный ключ: Employees.EmployeeID, Departments.DepartmentID");
//        Console.WriteLine("2. Внешний ключ: Employees.DepartmentID -> Departments.DepartmentID");
//        Console.WriteLine("3. Уникальное ограничение: Employees.EmployeeID, Departments.DepartmentID");
//        Console.WriteLine("4. Ограничение NOT NULL: Employees.DepartmentID, Employees.Name, Employees.Salary");
//        Console.WriteLine("5. Ограничение на значение: Employees.Salary >= 0");
//    }
//}


////Programm.cs
//using System;

//class Program
//{
//    static void Main()
//    {
//        EmployeeService employeeService = new EmployeeService();

//        // Пример 1: Добавление отдела
//        try
//        {
//            employeeService.AddDepartment("IT");
//            employeeService.AddDepartment("HR");
//            Console.WriteLine("Отделы добавлены.");
//        }
//        catch (Exception ex)
//        {
//            Console.WriteLine($"Ошибка: {ex.Message}");
//        }

//        // Пример 2: Добавление сотрудника
//        try
//        {
//            employeeService.AddEmployee(1, "Иван Иванов", 50000);
//            employeeService.AddEmployee(1, "Петр Петров", 60000);
//            Console.WriteLine("Сотрудники добавлены.");
//        }
//        catch (Exception ex)
//        {
//            Console.WriteLine($"Ошибка: {ex.Message}");
//        }

//        // Пример 3: Попытка нарушить ограничение (отрицательная зарплата)
//        try
//        {
//            employeeService.AddEmployee(1, "Сидор Сидоров", -1000);
//        }
//        catch (Exception ex)
//        {
//            Console.WriteLine($"Ошибка: {ex.Message}");
//        }

//        // Пример 4: Попытка нарушить внешний ключ (несуществующий отдел)
//        try
//        {
//            employeeService.AddEmployee(999, "Несуществующий сотрудник", 50000);
//        }
//        catch (Exception ex)
//        {
//            Console.WriteLine($"Ошибка: {ex.Message}");
//        }

//        // Пример 5: Отчёт об ограничениях
//        employeeService.PrintConstraintsReport();
//    }
//}


////26
////TypedEmployeeDataSet.cs
//using System;
//using System.Data;

//public partial class TypedEmployeeDataSet : DataSet
//{
//    private EmployeesDataTable tableEmployees;

//    public EmployeesDataTable Employees => tableEmployees;

//    public TypedEmployeeDataSet()
//    {
//        InitializeComponent();
//    }

//    private void InitializeComponent()
//    {
//        tableEmployees = new EmployeesDataTable("Employees");
//        this.Tables.Add(tableEmployees);
//    }
//}


////UntypedEmployeeDataSet.cs
//using System;
//using System.Data;

//public class UntypedEmployeeDataSet : DataSet
//{
//    public DataTable Employees { get; private set; }

//    public UntypedEmployeeDataSet()
//    {
//        InitializeComponent();
//    }

//    private void InitializeComponent()
//    {
//        Employees = new DataTable("Employees");

//        Employees.Columns.Add("EmployeeID", typeof(int));
//        Employees.Columns.Add("Name", typeof(string));
//        Employees.Columns.Add("Salary", typeof(decimal));

//        Employees.PrimaryKey = new DataColumn[] { Employees.Columns["EmployeeID"] };

//        this.Tables.Add(Employees);
//    }
//}


////PerformanceTest.cs
//using System;
//using System.Diagnostics;
//using System.Data;

//public class PerformanceTest
//{
//    private TypedEmployeeDataSet typedDataSet;
//    private UntypedEmployeeDataSet untypedDataSet;

//    public PerformanceTest()
//    {
//        typedDataSet = new TypedEmployeeDataSet();
//        untypedDataSet = new UntypedEmployeeDataSet();
//    }

//    public void FillDataSets(int recordCount)
//    {
//        // Заполнение типизированного DataSet
//        for (int i = 0; i < recordCount; i++)
//        {
//            typedDataSet.Employees.AddEmployeeRow($"Employee {i}", i * 1000);
//        }

//        // Заполнение нетипизированного DataSet
//        for (int i = 0; i < recordCount; i++)
//        {
//            DataRow row = untypedDataSet.Employees.NewRow();
//            row["EmployeeID"] = i;
//            row["Name"] = $"Employee {i}";
//            row["Salary"] = i * 1000;
//            untypedDataSet.Employees.Rows.Add(row);
//        }
//    }

//    public void MeasureAccessPerformance()
//    {
//        Stopwatch stopwatch = new Stopwatch();

//        // Типизированный доступ
//        stopwatch.Start();
//        for (int i = 0; i < typedDataSet.Employees.Rows.Count; i++)
//        {
//            string name = typedDataSet.Employees[i].Name;
//            decimal salary = typedDataSet.Employees[i].Salary;
//        }
//        stopwatch.Stop();
//        long typedAccessTime = stopwatch.ElapsedMilliseconds;

//        // Нетипизированный доступ
//        stopwatch.Restart();
//        foreach (DataRow row in untypedDataSet.Employees.Rows)
//        {
//            string name = (string)row["Name"];
//            decimal salary = (decimal)row["Salary"];
//        }
//        stopwatch.Stop();
//        long untypedAccessTime = stopwatch.ElapsedMilliseconds;

//        Console.WriteLine($"Доступ к значениям: Типизированный = {typedAccessTime} мс, Нетипизированный = {untypedAccessTime} мс");
//    }

//    public void MeasureSearchPerformance()
//    {
//        Stopwatch stopwatch = new Stopwatch();

//        // Типизированный поиск
//        stopwatch.Start();
//        var typedResult = typedDataSet.Employees.AsEnumerable()
//            .Where(row => row.Salary > 500000);
//        stopwatch.Stop();
//        long typedSearchTime = stopwatch.ElapsedMilliseconds;

//        // Нетипизированный поиск
//        stopwatch.Restart();
//        var untypedResult = untypedDataSet.Employees.AsEnumerable()
//            .Where(row => (decimal)row["Salary"] > 500000);
//        stopwatch.Stop();
//        long untypedSearchTime = stopwatch.ElapsedMilliseconds;

//        Console.WriteLine($"Поиск по критериям: Типизированный = {typedSearchTime} мс, Нетипизированный = {untypedSearchTime} мс");
//    }

//    public void MeasureAddPerformance()
//    {
//        Stopwatch stopwatch = new Stopwatch();

//        // Типизированное добавление
//        stopwatch.Start();
//        typedDataSet.Employees.AddEmployeeRow("New Employee", 100000);
//        stopwatch.Stop();
//        long typedAddTime = stopwatch.ElapsedMilliseconds;

//        // Нетипизированное добавление
//        stopwatch.Restart();
//        DataRow row = untypedDataSet.Employees.NewRow();
//        row["EmployeeID"] = untypedDataSet.Employees.Rows.Count;
//        row["Name"] = "New Employee";
//        row["Salary"] = 100000;
//        untypedDataSet.Employees.Rows.Add(row);
//        stopwatch.Stop();
//        long untypedAddTime = stopwatch.ElapsedMilliseconds;

//        Console.WriteLine($"Добавление новой строки: Типизированный = {typedAddTime} мс, Нетипизированный = {untypedAddTime} мс");
//    }

//    public void MeasureEditPerformance()
//    {
//        Stopwatch stopwatch = new Stopwatch();

//        // Типизированное редактирование
//        stopwatch.Start();
//        if (typedDataSet.Employees.Rows.Count > 0)
//            typedDataSet.Employees[0].Salary = 200000;
//        stopwatch.Stop();
//        long typedEditTime = stopwatch.ElapsedMilliseconds;

//        // Нетипизированное редактирование
//        stopwatch.Restart();
//        if (untypedDataSet.Employees.Rows.Count > 0)
//            untypedDataSet.Employees.Rows[0]["Salary"] = 200000;
//        stopwatch.Stop();
//        long untypedEditTime = stopwatch.ElapsedMilliseconds;

//        Console.WriteLine($"Редактирование строки: Типизированный = {typedEditTime} мс, Нетипизированный = {untypedEditTime} мс");
//    }

//    public void MeasureDeletePerformance()
//    {
//        Stopwatch stopwatch = new Stopwatch();

//        // Типизированное удаление
//        stopwatch.Start();
//        if (typedDataSet.Employees.Rows.Count > 0)
//            typedDataSet.Employees[0].Row.Delete();
//        stopwatch.Stop();
//        long typedDeleteTime = stopwatch.ElapsedMilliseconds;

//        // Нетипизированное удаление
//        stopwatch.Restart();
//        if (untypedDataSet.Employees.Rows.Count > 0)
//            untypedDataSet.Employees.Rows[0].Delete();
//        stopwatch.Stop();
//        long untypedDeleteTime = stopwatch.ElapsedMilliseconds;

//        Console.WriteLine($"Удаление строки: Типизированный = {typedDeleteTime} мс, Нетипизированный = {untypedDeleteTime} мс");
//    }
//}


////Programm.cs
//using System;

//class Program
//{
//    static void Main()
//    {
//        PerformanceTest performanceTest = new PerformanceTest();

//        // Заполнение DataSet 1,000,000 записями
//        performanceTest.FillDataSets(1000000);

//        // Измерение производительности
//        performanceTest.MeasureAccessPerformance();
//        performanceTest.MeasureSearchPerformance();
//        performanceTest.MeasureAddPerformance();
//        performanceTest.MeasureEditPerformance();
//        performanceTest.MeasureDeletePerformance();

//        // Таблица сравнения
//        Console.WriteLine("\nТаблица сравнения производительности:");
//        Console.WriteLine("| Операция               | Типизированный (мс) | Нетипизированный (мс) |");
//        Console.WriteLine("|-------------------------|----------------------|------------------------|");
//        Console.WriteLine("| Доступ к значениям     | {0}                    | {1}                     |", GetTime(performanceTest, "Access"));
//        Console.WriteLine("| Поиск по критериям     | {0}                    | {1}                     |", GetTime(performanceTest, "Search"));
//        Console.WriteLine("| Добавление строки      | {0}                    | {1}                     |", GetTime(performanceTest, "Add"));
//        Console.WriteLine("| Редактирование строки  | {0}                    | {1}                     |", GetTime(performanceTest, "Edit"));
//        Console.WriteLine("| Удаление строки        | {0}                    | {1}                     |", GetTime(performanceTest, "Delete"));
//    }

//    private static string GetTime(PerformanceTest test, string operation)
//    {
//        // Для упрощения возвращаем фиктивные значения
//        return "N/A";
//    }
//}


////27
////EmployeeDataSet.cs
//using System;
//using System.Data;

//public partial class EmployeeDataSet : DataSet
//{
//    private EmployeesDataTable tableEmployees;

//    public EmployeesDataTable Employees => tableEmployees;

//    public EmployeeDataSet()
//    {
//        InitializeComponent();
//    }

//    private void InitializeComponent()
//    {
//        tableEmployees = new EmployeesDataTable("Employees");
//        this.Tables.Add(tableEmployees);
//    }
//}


////28
////EmployeesDataTable.cs
//using System;
//using System.Data;

//public partial class EmployeesDataTable : DataTable
//{
//    public EmployeesDataTable() : base("Employees") { InitializeColumns(); }
//    public EmployeesDataTable(string tableName) : base(tableName) { InitializeColumns(); }

//    private void InitializeColumns()
//    {
//        DataColumn columnEmployeeID = new DataColumn("EmployeeID", typeof(int)) { AllowDBNull = false, AutoIncrement = true };
//        DataColumn columnName = new DataColumn("Name", typeof(string)) { AllowDBNull = false, MaxLength = 100 };
//        DataColumn columnSalary = new DataColumn("Salary", typeof(decimal)) { AllowDBNull = false };

//        this.Columns.AddRange(new DataColumn[] { columnEmployeeID, columnName, columnSalary });
//        this.PrimaryKey = new DataColumn[] { columnEmployeeID };
//    }

//    public EmployeeRow AddEmployeeRow(string name, decimal salary)
//    {
//        EmployeeRow row = new EmployeeRow(this.NewRow());
//        row.Name = name;
//        row.Salary = salary;
//        this.Rows.Add(row.Row);
//        return row;
//    }
//}


////EmployeeRow.cs
//using System;
//using System.Data;

//public class EmployeeRow
//{
//    private DataRow row;
//    public EmployeeRow(DataRow row) { this.row = row; }

//    public int EmployeeID { get => (int)row["EmployeeID"]; set => row["EmployeeID"] = value; }
//    public string Name { get => (string)row["Name"]; set => row["Name"] = value; }
//    public decimal Salary { get => (decimal)row["Salary"]; set => row["Salary"] = value; }
//    public DataRow Row => row;
//}


////MainForm.cs
//using System;
//using System.ComponentModel;
//using System.Data;
//using System.Threading;
//using System.Threading.Tasks;
//using System.Windows.Forms;

//namespace AsyncDataLoadingApp
//{
//    public partial class MainForm : Form
//    {
//        private EmployeeDataSet employeeDataSet;
//        private CancellationTokenSource cancellationTokenSource;

//        public MainForm()
//        {
//            InitializeComponent();
//            employeeDataSet = new EmployeeDataSet();
//        }

//        // Асинхронная загрузка с использованием Task
//        private async void btnLoadWithTask_Click(object sender, EventArgs e)
//        {
//            btnLoadWithTask.Enabled = false;
//            btnCancel.Enabled = true;
//            progressBar1.Value = 0;
//            cancellationTokenSource = new CancellationTokenSource();

//            try
//            {
//                await LoadDataWithTaskAsync(cancellationTokenSource.Token);
//                dataGridView1.DataSource = employeeDataSet.Employees;
//                MessageBox.Show("Данные загружены успешно!");
//            }
//            catch (OperationCanceledException)
//            {
//                MessageBox.Show("Загрузка отменена пользователем.");
//            }
//            catch (Exception ex)
//            {
//                MessageBox.Show($"Ошибка: {ex.Message}");
//            }
//            finally
//            {
//                btnLoadWithTask.Enabled = true;
//                btnCancel.Enabled = false;
//                cancellationTokenSource.Dispose();
//            }
//        }

//        private async Task LoadDataWithTaskAsync(CancellationToken cancellationToken)
//        {
//            await Task.Run(() =>
//            {
//                for (int i = 0; i < 10000; i++)
//                {
//                    if (cancellationToken.IsCancellationRequested)
//                        cancellationToken.ThrowIfCancellationRequested();

//                    employeeDataSet.Employees.AddEmployeeRow($"Employee {i}", i * 100);
//                    int progress = (i + 1) * 100 / 10000;
//                    this.Invoke((MethodInvoker)delegate { progressBar1.Value = progress; });
//                    Thread.Sleep(1); // Имитация задержки
//                }
//            }, cancellationToken);
//        }

//        // Асинхронная загрузка с использованием BackgroundWorker
//        private void btnLoadWithBackgroundWorker_Click(object sender, EventArgs e)
//        {
//            btnLoadWithBackgroundWorker.Enabled = false;
//            btnCancel.Enabled = true;
//            progressBar1.Value = 0;

//            BackgroundWorker worker = new BackgroundWorker
//            {
//                WorkerReportsProgress = true,
//                WorkerSupportsCancellation = true
//            };

//            worker.DoWork += (s, args) =>
//            {
//                BackgroundWorker bw = s as BackgroundWorker;
//                for (int i = 0; i < 10000; i++)
//                {
//                    if (bw.CancellationPending)
//                    {
//                        args.Cancel = true;
//                        return;
//                    }

//                    employeeDataSet.Employees.AddEmployeeRow($"Employee {i}", i * 100);
//                    bw.ReportProgress((i + 1) * 100 / 10000);
//                    Thread.Sleep(1); // Имитация задержки
//                }
//            };

//            worker.ProgressChanged += (s, args) =>
//            {
//                progressBar1.Value = args.ProgressPercentage;
//            };

//            worker.RunWorkerCompleted += (s, args) =>
//            {
//                btnLoadWithBackgroundWorker.Enabled = true;
//                btnCancel.Enabled = false;

//                if (args.Cancelled)
//                {
//                    MessageBox.Show("Загрузка отменена пользователем.");
//                }
//                else if (args.Error != null)
//                {
//                    MessageBox.Show($"Ошибка: {args.Error.Message}");
//                }
//                else
//                {
//                    dataGridView1.DataSource = employeeDataSet.Employees;
//                    MessageBox.Show("Данные загружены успешно!");
//                }
//            };

//            worker.RunWorkerAsync();
//        }

//        private void btnCancel_Click(object sender, EventArgs e)
//        {
//            if (cancellationTokenSource != null)
//                cancellationTokenSource.Cancel();

//            // Для BackgroundWorker отмена реализована в DoWork
//        }
//    }
//}


////28
////EmployeeDataSet.cs
//using System;
//using System.Data;

//public partial class EmployeeDataSet : DataSet
//{
//    private EmployeesDataTable tableEmployees;

//    public EmployeesDataTable Employees => tableEmployees;

//    public EmployeeDataSet()
//    {
//        InitializeComponent();
//    }

//    private void InitializeComponent()
//    {
//        tableEmployees = new EmployeesDataTable("Employees");
//        this.Tables.Add(tableEmployees);
//    }
//}


////DataExporter.cs
//using System;
//using System.Data;
//using System.IO;
//using System.Xml;
//using Newtonsoft.Json;
//using CsvHelper;
//using OfficeOpenXml;

//public static class DataExporter
//{
//    // Экспорт в XML
//    public static void ExportToXml(DataTable dataTable, string filePath)
//    {
//        dataTable.WriteXml(filePath);
//    }

//    // Импорт из XML
//    public static void ImportFromXml(DataTable dataTable, string filePath)
//    {
//        dataTable.ReadXml(filePath);
//    }

//    // Экспорт в CSV
//    public static void ExportToCsv(DataTable dataTable, string filePath)
//    {
//        using (var writer = new StreamWriter(filePath))
//        using (var csv = new CsvWriter(writer, System.Globalization.CultureInfo.InvariantCulture))
//        {
//            foreach (DataColumn column in dataTable.Columns)
//                csv.WriteField(column.ColumnName);
//            csv.NextRecord();

//            foreach (DataRow row in dataTable.Rows)
//            {
//                foreach (DataColumn column in dataTable.Columns)
//                    csv.WriteField(row[column]);
//                csv.NextRecord();
//            }
//        }
//    }

//    // Импорт из CSV
//    public static void ImportFromCsv(DataTable dataTable, string filePath)
//    {
//        using (var reader = new StreamReader(filePath))
//        using (var csv = new CsvReader(reader, System.Globalization.CultureInfo.InvariantCulture))
//        {
//            csv.Read();
//            csv.ReadHeader();

//            while (csv.Read())
//            {
//                DataRow row = dataTable.NewRow();
//                foreach (DataColumn column in dataTable.Columns)
//                    row[column.ColumnName] = csv.GetField(column.ColumnName);
//                dataTable.Rows.Add(row);
//            }
//        }
//    }

//    // Экспорт в Excel
//    public static void ExportToExcel(DataTable dataTable, string filePath)
//    {
//        using (var package = new ExcelPackage(new FileInfo(filePath)))
//        {
//            var worksheet = package.Workbook.Worksheets.Add("Employees");
//            worksheet.Cells.LoadFromDataTable(dataTable, true);
//            package.Save();
//        }
//    }

//    // Импорт из Excel
//    public static void ImportFromExcel(DataTable dataTable, string filePath)
//    {
//        using (var package = new ExcelPackage(new FileInfo(filePath)))
//        {
//            var worksheet = package.Workbook.Worksheets[0];
//            for (int row = 2; row <= worksheet.Dimension.End.Row; row++)
//            {
//                DataRow dataRow = dataTable.NewRow();
//                for (int col = 1; col <= worksheet.Dimension.End.Column; col++)
//                    dataRow[col - 1] = worksheet.Cells[row, col].Value;
//                dataTable.Rows.Add(dataRow);
//            }
//        }
//    }

//    // Экспорт в JSON
//    public static void ExportToJson(DataTable dataTable, string filePath)
//    {
//        string json = JsonConvert.SerializeObject(dataTable, Formatting.Indented);
//        File.WriteAllText(filePath, json);
//    }

//    // Импорт из JSON
//    public static void ImportFromJson(DataTable dataTable, string filePath)
//    {
//        string json = File.ReadAllText(filePath);
//        DataTable importedTable = JsonConvert.DeserializeObject<DataTable>(json);
//        dataTable.Merge(importedTable);
//    }
//}


////MainForm.csusing System;
//using System.Data;
//using System.Windows.Forms;

//namespace DataExportImportApp
//{
//    public partial class MainForm : Form
//    {
//        private EmployeeDataSet employeeDataSet;

//        public MainForm()
//        {
//            InitializeComponent();
//            employeeDataSet = new EmployeeDataSet();
//            LoadSampleData();
//        }

//        private void LoadSampleData()
//        {
//            employeeDataSet.Employees.AddEmployeeRow("Иван Иванов", 50000);
//            employeeDataSet.Employees.AddEmployeeRow("Петр Петров", 60000);
//            dataGridView1.DataSource = employeeDataSet.Employees;
//        }

//        private void btnExportXml_Click(object sender, EventArgs e)
//        {
//            SaveFileDialog saveFileDialog = new SaveFileDialog { Filter = "XML files (*.xml)|*.xml" };
//            if (saveFileDialog.ShowDialog() == DialogResult.OK)
//            {
//                try
//                {
//                    DataExporter.ExportToXml(employeeDataSet.Employees, saveFileDialog.FileName);
//                    MessageBox.Show("Экспорт в XML выполнен успешно!");
//                }
//                catch (Exception ex)
//                {
//                    MessageBox.Show($"Ошибка: {ex.Message}");
//                }
//            }
//        }

//        private void btnImportXml_Click(object sender, EventArgs e)
//        {
//            OpenFileDialog openFileDialog = new OpenFileDialog { Filter = "XML files (*.xml)|*.xml" };
//            if (openFileDialog.ShowDialog() == DialogResult.OK)
//            {
//                try
//                {
//                    employeeDataSet.Employees.Clear();
//                    DataExporter.ImportFromXml(employeeDataSet.Employees, openFileDialog.FileName);
//                    dataGridView1.DataSource = employeeDataSet.Employees;
//                    MessageBox.Show("Импорт из XML выполнен успешно!");
//                }
//                catch (Exception ex)
//                {
//                    MessageBox.Show($"Ошибка: {ex.Message}");
//                }
//            }
//        }

//        private void btnExportCsv_Click(object sender, EventArgs e)
//        {
//            SaveFileDialog saveFileDialog = new SaveFileDialog { Filter = "CSV files (*.csv)|*.csv" };
//            if (saveFileDialog.ShowDialog() == DialogResult.OK)
//            {
//                try
//                {
//                    DataExporter.ExportToCsv(employeeDataSet.Employees, saveFileDialog.FileName);
//                    MessageBox.Show("Экспорт в CSV выполнен успешно!");
//                }
//                catch (Exception ex)
//                {
//                    MessageBox.Show($"Ошибка: {ex.Message}");
//                }
//            }
//        }

//        private void btnImportCsv_Click(object sender, EventArgs e)
//        {
//            OpenFileDialog openFileDialog = new OpenFileDialog { Filter = "CSV files (*.csv)|*.csv" };
//            if (openFileDialog.ShowDialog() == DialogResult.OK)
//            {
//                try
//                {
//                    employeeDataSet.Employees.Clear();
//                    DataExporter.ImportFromCsv(employeeDataSet.Employees, openFileDialog.FileName);
//                    dataGridView1.DataSource = employeeDataSet.Employees;
//                    MessageBox.Show("Импорт из CSV выполнен успешно!");
//                }
//                catch (Exception ex)
//                {
//                    MessageBox.Show($"Ошибка: {ex.Message}");
//                }
//            }
//        }

//        private void btnExportExcel_Click(object sender, EventArgs e)
//        {
//            SaveFileDialog saveFileDialog = new SaveFileDialog { Filter = "Excel files (*.xlsx)|*.xlsx" };
//            if (saveFileDialog.ShowDialog() == DialogResult.OK)
//            {
//                try
//                {
//                    DataExporter.ExportToExcel(employeeDataSet.Employees, saveFileDialog.FileName);
//                    MessageBox.Show("Экспорт в Excel выполнен успешно!");
//                }
//                catch (Exception ex)
//                {
//                    MessageBox.Show($"Ошибка: {ex.Message}");
//                }
//            }
//        }

//        private void btnImportExcel_Click(object sender, EventArgs e)
//        {
//            OpenFileDialog openFileDialog = new OpenFileDialog { Filter = "Excel files (*.xlsx)|*.xlsx" };
//            if (openFileDialog.ShowDialog() == DialogResult.OK)
//            {
//                try
//                {
//                    employeeDataSet.Employees.Clear();
//                    DataExporter.ImportFromExcel(employeeDataSet.Employees, openFileDialog.FileName);
//                    dataGridView1.DataSource = employeeDataSet.Employees;
//                    MessageBox.Show("Импорт из Excel выполнен успешно!");
//                }
//                catch (Exception ex)
//                {
//                    MessageBox.Show($"Ошибка: {ex.Message}");
//                }
//            }
//        }

//        private void btnExportJson_Click(object sender, EventArgs e)
//        {
//            SaveFileDialog saveFileDialog = new SaveFileDialog { Filter = "JSON files (*.json)|*.json" };
//            if (saveFileDialog.ShowDialog() == DialogResult.OK)
//            {
//                try
//                {
//                    DataExporter.ExportToJson(employeeDataSet.Employees, saveFileDialog.FileName);
//                    MessageBox.Show("Экспорт в JSON выполнен успешно!");
//                }
//                catch (Exception ex)
//                {
//                    MessageBox.Show($"Ошибка: {ex.Message}");
//                }
//            }
//        }

//        private void btnImportJson_Click(object sender, EventArgs e)
//        {
//            OpenFileDialog openFileDialog = new OpenFileDialog { Filter = "JSON files (*.json)|*.json" };
//            if (openFileDialog.ShowDialog() == DialogResult.OK)
//            {
//                try
//                {
//                    employeeDataSet.Employees.Clear();
//                    DataExporter.ImportFromJson(employeeDataSet.Employees, openFileDialog.FileName);
//                    dataGridView1.DataSource = employeeDataSet.Employees;
//                    MessageBox.Show("Импорт из JSON выполнен успешно!");
//                }
//                catch (Exception ex)
//                {
//                    MessageBox.Show($"Ошибка: {ex.Message}");
//                }
//            }
//        }
//    }
//}


////29
////IEmployeeService.cs
//using System;
//using System.Data;
//using System.ServiceModel;

//[ServiceContract]
//public interface IEmployeeService
//{
//    [OperationContract]
//    EmployeeDataSet GetEmployees();

//    [OperationContract]
//    bool SaveEmployees(EmployeeDataSet employees);

//    [OperationContract]
//    string GetLastError();
//}


////EmployeeService.cs
//using System;
//using System.Data;
//using System.Data.SqlClient;
//using System.ServiceModel;

//public class EmployeeService : IEmployeeService
//{
//    private string connectionString = "Your_Connection_String";
//    private string lastError = string.Empty;

//    public EmployeeDataSet GetEmployees()
//    {
//        EmployeeDataSet dataSet = new EmployeeDataSet();
//        try
//        {
//            using (SqlConnection connection = new SqlConnection(connectionString))
//            {
//                connection.Open();
//                SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM Employees", connection);
//                adapter.Fill(dataSet, "Employees");
//            }
//        }
//        catch (Exception ex)
//        {
//            lastError = $"Ошибка при получении данных: {ex.Message}";
//            throw new FaultException(lastError);
//        }
//        return dataSet;
//    }

//    public bool SaveEmployees(EmployeeDataSet employees)
//    {
//        try
//        {
//            using (SqlConnection connection = new SqlConnection(connectionString))
//            {
//                connection.Open();
//                SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM Employees", connection);
//                SqlCommandBuilder builder = new SqlCommandBuilder(adapter);
//                adapter.Update(employees.Employees);
//            }
//            return true;
//        }
//        catch (DBConcurrencyException)
//        {
//            lastError = "Конфликт при сохранении данных: запись была изменена другим пользователем.";
//            throw new FaultException(lastError);
//        }
//        catch (Exception ex)
//        {
//            lastError = $"Ошибка при сохранении данных: {ex.Message}";
//            throw new FaultException(lastError);
//        }
//    }

//    public string GetLastError()
//    {
//        return lastError;
//    }
//}


////EmployeeClient.cs
//using System;
//using System.ServiceModel;
//using System.Windows.Forms;

//namespace EmployeeClientApp
//{
//    public partial class MainForm : Form
//    {
//        private EmployeeServiceClient client;
//        private EmployeeDataSet employeeDataSet;

//        public MainForm()
//        {
//            InitializeComponent();
//            client = new EmployeeServiceClient();
//            employeeDataSet = new EmployeeDataSet();
//        }

//        private void btnLoad_Click(object sender, EventArgs e)
//        {
//            try
//            {
//                employeeDataSet = client.GetEmployees();
//                dataGridView1.DataSource = employeeDataSet.Employees;
//            }
//            catch (FaultException<ExceptionDetail> ex)
//            {
//                MessageBox.Show($"Ошибка сервера: {ex.Detail.Message}");
//            }
//            catch (CommunicationException ex)
//            {
//                MessageBox.Show($"Ошибка связи: {ex.Message}");
//            }
//            catch (TimeoutException ex)
//            {
//                MessageBox.Show($"Таймаут: {ex.Message}");
//            }
//            catch (Exception ex)
//            {
//                MessageBox.Show($"Ошибка: {ex.Message}");
//            }
//        }

//        private void btnSave_Click(object sender, EventArgs e)
//        {
//            try
//            {
//                bool success = client.SaveEmployees(employeeDataSet);
//                if (success)
//                {
//                    MessageBox.Show("Данные успешно сохранены!");
//                    employeeDataSet.AcceptChanges();
//                }
//            }
//            catch (FaultException<ExceptionDetail> ex)
//            {
//                MessageBox.Show($"Ошибка сервера: {ex.Detail.Message}");
//                employeeDataSet.RejectChanges();
//            }
//            catch (CommunicationException ex)
//            {
//                MessageBox.Show($"Ошибка связи: {ex.Message}");
//            }
//            catch (TimeoutException ex)
//            {
//                MessageBox.Show($"Таймаут: {ex.Message}");
//            }
//            catch (Exception ex)
//            {
//                MessageBox.Show($"Ошибка: {ex.Message}");
//            }
//        }
//    }
//}


////30
////EmployeeDataSetCache.cs
//using System;
//using System.Data;
//using System.Runtime.Caching;

//public class EmployeeDataSetCache
//{
//    private static ObjectCache cache = MemoryCache.Default;
//    private static string cacheKey = "EmployeeDataSetCache";
//    private static DateTimeOffset cacheExpiration = DateTimeOffset.Now.AddMinutes(10);

//    public static EmployeeDataSet GetFromCache()
//    {
//        return cache.Get(cacheKey) as EmployeeDataSet;
//    }

//    public static void AddToCache(EmployeeDataSet dataSet)
//    {
//        cache.Set(cacheKey, dataSet, cacheExpiration);
//    }

//    public static void ClearCache()
//    {
//        cache.Remove(cacheKey);
//    }

//    public static bool IsCacheValid()
//    {
//        return cache.Get(cacheKey) != null;
//    }
//}


////EmployeeServiceWithCache.cs
//using System;
//using System.Data;
//using System.Data.SqlClient;
//using System.Diagnostics;

//public class EmployeeServiceWithCache
//{
//    private string connectionString = "Your_Connection_String";

//    public EmployeeDataSet GetEmployees()
//    {
//        EmployeeDataSet cachedDataSet = EmployeeDataSetCache.GetFromCache();
//        if (cachedDataSet != null)
//        {
//            Console.WriteLine("Данные получены из кэша.");
//            return cachedDataSet;
//        }

//        EmployeeDataSet dataSet = new EmployeeDataSet();
//        Stopwatch stopwatch = Stopwatch.StartNew();

//        try
//        {
//            using (SqlConnection connection = new SqlConnection(connectionString))
//            {
//                connection.Open();
//                SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM Employees", connection);
//                adapter.Fill(dataSet, "Employees");
//            }

//            EmployeeDataSetCache.AddToCache(dataSet);
//            stopwatch.Stop();
//            Console.WriteLine($"Данные загружены из базы данных за {stopwatch.ElapsedMilliseconds} мс.");
//        }
//        catch (Exception ex)
//        {
//            Console.WriteLine($"Ошибка при получении данных: {ex.Message}");
//        }

//        return dataSet;
//    }

//    public bool SaveEmployees(EmployeeDataSet employees)
//    {
//        try
//        {
//            using (SqlConnection connection = new SqlConnection(connectionString))
//            {
//                connection.Open();
//                SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM Employees", connection);
//                SqlCommandBuilder builder = new SqlCommandBuilder(adapter);
//                adapter.Update(employees.Employees);
//            }

//            EmployeeDataSetCache.ClearCache();
//            Console.WriteLine("Кэш очищен после сохранения данных.");
//            return true;
//        }
//        catch (Exception ex)
//        {
//            Console.WriteLine($"Ошибка при сохранении данных: {ex.Message}");
//            return false;
//        }
//    }

//    public void RefreshCache()
//    {
//        EmployeeDataSetCache.ClearCache();
//        Console.WriteLine("Кэш обновлён.");
//    }
//}


////Programm.cs
//using System;
//using System.Diagnostics;

//class Program
//{
//    static void Main()
//    {
//        EmployeeServiceWithCache service = new EmployeeServiceWithCache();

//        // Первая загрузка данных (из базы данных)
//        EmployeeDataSet firstLoad = service.GetEmployees();

//        // Вторая загрузка данных (из кэша)
//        EmployeeDataSet secondLoad = service.GetEmployees();

//        // Сохранение изменений и очистка кэша
//        service.SaveEmployees(firstLoad);

//        // Обновление кэша
//        service.RefreshCache();

//        // Третья загрузка данных (из базы данных, так как кэш очищен)
//        EmployeeDataSet thirdLoad = service.GetEmployees();
//    }
//}


////31
////EmployeeDataSet.cs
//using System;
//using System.Data;

//public partial class EmployeeDataSet : DataSet
//{
//    private EmployeesDataTable tableEmployees;

//    public EmployeesDataTable Employees => tableEmployees;

//    public EmployeeDataSet()
//    {
//        InitializeComponent();
//    }

//    private void InitializeComponent()
//    {
//        tableEmployees = new EmployeesDataTable("Employees");
//        this.Tables.Add(tableEmployees);
//    }
//}


////EmployeesDataTable.cs
//using System;
//using System.Data;

//public partial class EmployeesDataTable : DataTable
//{
//    public EmployeesDataTable() : base("Employees") { InitializeColumns(); }
//    public EmployeesDataTable(string tableName) : base(tableName) { InitializeColumns(); }

//    private void InitializeColumns()
//    {
//        DataColumn columnEmployeeID = new DataColumn("EmployeeID", typeof(int)) { AllowDBNull = false };
//        DataColumn columnName = new DataColumn("Name", typeof(string)) { AllowDBNull = false, MaxLength = 100 };
//        DataColumn columnSalary = new DataColumn("Salary", typeof(decimal)) { AllowDBNull = false };
//        DataColumn columnSource = new DataColumn("Source", typeof(string)) { AllowDBNull = false, MaxLength = 50 };

//        this.Columns.AddRange(new DataColumn[] { columnEmployeeID, columnName, columnSalary, columnSource });
//        this.PrimaryKey = new DataColumn[] { columnEmployeeID };
//    }

//    public EmployeeRow AddEmployeeRow(int employeeID, string name, decimal salary, string source)
//    {
//        EmployeeRow row = new EmployeeRow(this.NewRow());
//        row.EmployeeID = employeeID;
//        row.Name = name;
//        row.Salary = salary;
//        row.Source = source;
//        this.Rows.Add(row.Row);
//        return row;
//    }
//}


////EmployeeRow.cs
//using System;
//using System.Data;

//public class EmployeeRow
//{
//    private DataRow row;
//    public EmployeeRow(DataRow row) { this.row = row; }

//    public int EmployeeID { get => (int)row["EmployeeID"]; set => row["EmployeeID"] = value; }
//    public string Name { get => (string)row["Name"]; set => row["Name"] = value; }
//    public decimal Salary { get => (decimal)row["Salary"]; set => row["Salary"] = value; }
//    public string Source { get => (string)row["Source"]; set => row["Source"] = value; }
//    public DataRow Row => row;
//}


////DataLoader.cs
//using System;
//using System.Data;
//using System.Data.SqlClient;
//using System.Xml;
//using OfficeOpenXml;
//using System.IO;

//public static class DataLoader
//{
//    // Загрузка данных из SQL Server
//    public static void LoadFromSql(EmployeesDataTable employees, string connectionString)
//    {
//        using (SqlConnection connection = new SqlConnection(connectionString))
//        {
//            connection.Open();
//            SqlDataAdapter adapter = new SqlDataAdapter("SELECT EmployeeID, Name, Salary FROM Employees", connection);
//            DataTable tempTable = new DataTable();
//            adapter.Fill(tempTable);

//            foreach (DataRow row in tempTable.Rows)
//            {
//                employees.AddEmployeeRow(
//                    (int)row["EmployeeID"],
//                    (string)row["Name"],
//                    (decimal)row["Salary"],
//                    "SQL");
//            }
//        }
//    }

//    // Загрузка данных из Excel
//    public static void LoadFromExcel(EmployeesDataTable employees, string filePath)
//    {
//        using (var package = new ExcelPackage(new FileInfo(filePath)))
//        {
//            var worksheet = package.Workbook.Worksheets[0];
//            for (int row = 2; row <= worksheet.Dimension.End.Row; row++)
//            {
//                employees.AddEmployeeRow(
//                    Convert.ToInt32(worksheet.Cells[row, 1].Value),
//                    worksheet.Cells[row, 2].Value.ToString(),
//                    Convert.ToDecimal(worksheet.Cells[row, 3].Value),
//                    "Excel");
//            }
//        }
//    }

//    // Загрузка данных из XML
//    public static void LoadFromXml(EmployeesDataTable employees, string filePath)
//    {
//        DataSet tempDataSet = new DataSet();
//        tempDataSet.ReadXml(filePath);

//        foreach (DataRow row in tempDataSet.Tables[0].Rows)
//        {
//            employees.AddEmployeeRow(
//                (int)row["EmployeeID"],
//                (string)row["Name"],
//                (decimal)row["Salary"],
//                "XML");
//        }
//    }
//}


////DataMerger.cs
//using System;
//using System.Data;

//public static class DataMerger
//{
//    public static void MergeDataSets(EmployeeDataSet target, EmployeeDataSet source)
//    {
//        foreach (EmployeeRow sourceRow in source.Employees)
//        {
//            EmployeeRow targetRow = target.Employees.FindByID(sourceRow.EmployeeID);

//            if (targetRow == null)
//            {
//                // Если записи нет, добавляем новую
//                target.Employees.AddEmployeeRow(
//                    sourceRow.EmployeeID,
//                    sourceRow.Name,
//                    sourceRow.Salary,
//                    sourceRow.Source);
//            }
//            else
//            {
//                // Если запись есть, разрешаем конфликт (например, берем данные из источника с приоритетом)
//                if (sourceRow.Source == "SQL")
//                {
//                    targetRow.Name = sourceRow.Name;
//                    targetRow.Salary = sourceRow.Salary;
//                    targetRow.Source = sourceRow.Source;
//                }
//            }
//        }
//    }

//    public static void SyncDataSets(EmployeeDataSet target, EmployeeDataSet source)
//    {
//        // Синхронизация данных: обновление измененных записей
//        foreach (EmployeeRow sourceRow in source.Employees)
//        {
//            EmployeeRow targetRow = target.Employees.FindByID(sourceRow.EmployeeID);

//            if (targetRow != null)
//            {
//                targetRow.Name = sourceRow.Name;
//                targetRow.Salary = sourceRow.Salary;
//                targetRow.Source = sourceRow.Source;
//            }
//        }
//    }
//}
