
using Dapper;
using Lesson_13.Models;
using Lesson_13.Helpers;
using DBHelper;
using COMMON;



ElordaSingleton.GetInstance.SetConnectionString("server=localhost;database=music_db;user=music_dba;password=87654321;charset=utf8mb4");
SimpleCRUD.SetDialect(SimpleCRUD.Dialect.MySQL);
SimpleCRUD.SetTableNameResolver(new ElordaResolver());




using (var connection = Utilities.GetOpenConnection())
{

    // Artist artist = connection.GetList<Artist>("where qStatus = @qStatus and artistName = @artistName ", new {qStatus = 0, artistName = "Erlan"}).FirstOrDefault();
    // if(artist != null)
    // {
    //     artist.ArtistName = "Erlan Izbasar";
    //     artist.UpdateTime = 1;
    //     artist.QStatus = 1;
    //     int res = connection.Update<Artist>(artist);
    //     if(res>0){
    //          Console.WriteLine("Updated artist successfully");
    //     }
    // }
    int page = 1;
    int pageSize = 20;
    var sql = "where qStatus  = 0 order by addTime asc limit @start,@length ";
    object param = new { start = (page - 1) * pageSize, length = pageSize };
    

    List<Artist> artistList = connection.GetList<Artist>(sql, param).ToList();
    foreach (Artist artist in artistList)
    {
        Console.WriteLine(artist.ArtistName);
    }
//    int? res =  connection.Insert<Artist>(new Artist()
//     {
//         AddTime = 0,
//         ArtistIds = "",
//         ArtistIntroduction = "",
//         ArtistName = "Erlan",
//         FavoriteCount = 0,
//         Gender = 1,
//         GroupType = 1,
//         IdName = "",
//         QStatus = 0,
//         SearchName = "",
//         ThumbnailUrl = "",
//         TopTime = 0,
//         UpdateTime = 0,
//     });

// if(res>0){
//     Console.WriteLine("Artist added successfully!");
// }
   




}















// string email = "admin@music.com";
// string password = "12345678' or '1' = '1";
// string userLoginSql = $"select count(1) from admin where qStatus = 0 and email = '{email}' and password = '{password}'";
// int result = connection.Execute("update artist set qStatus = 1 where qStatus = 0 and artistName = @artistName;", new {artistName = "Azamat Abıldaev" });
// if(result >0){

// }
// var sql = "SELECT id,artistName,thumbnailUrl,idName,addTime FROM artist where qStatus  = 0 order by addTime desc limit @start,@length ";
// object param = new {start = (page-1)*pageSize, length = pageSize};
// var result = connection.Query<Artist>(sql,param).ToList();
// foreach (var artist in result)
// {
//     Console.WriteLine("Artist: " + artist.ArtistName);
// }









// MySqlConnection connection = new MySqlConnection(connectionString);

// try
// {
//     connection.Open();
//     string query = "SELECT * FROM artist WHERE qStatus = 0";
//     MySqlCommand cmd = new MySqlCommand(query, connection);
//     MySqlDataReader dataReader = cmd.ExecuteReader();

//     while (dataReader.Read())
//     {
//         Console.WriteLine(dataReader["artistName"].ToString());
//     }
// }
// catch (MySqlException ex)
// {
//     // Handle any errors
//     Console.WriteLine("Error: " + ex.Message);
// }
// finally
// {
//     connection.Close();
// }