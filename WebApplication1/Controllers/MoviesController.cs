using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApplication1.Models;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web;

namespace WebApplication1.Controllers
{
    public class MoviesController : ApiController
    {

        public HttpResponseMessage Get()
        {
            // Here we fetch the data from the sql server from the Movies table
            string query = @"
                    select MovieId, MovieTitle, Genre,
                    convert(varchar(10), DateOfRelease,120) as DateOfRelease,
                    PhotoFileName
                    from
                    dbo.Movies
                    ";
            DataTable table = new DataTable();
            using (var con = new SqlConnection(ConfigurationManager.
                ConnectionStrings["MovieAppDB"].ConnectionString))// This uses the connection string to pin point  database
            using (var cmd = new SqlCommand(query, con))// We tell c# that we are using sql commands
            using (var da = new SqlDataAdapter(cmd))// We use the sql data adapter wich helps fill our table
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);// This fills our database
            }

            return Request.CreateResponse(HttpStatusCode.OK, table);// This just returns a success status


        }
        public string Post(Movies mad)
        {
            try
            {
                // Here we insert data into the table Movies
                string query = @"
                    insert into dbo.Movies values
                    ('" + mad.MovieTitle + @"'
                    ,'" + mad.DateOfRelease + @"'
                    ,'" + mad.Genre + @"'
                    ,'"+  mad.PhotoFileName + @"')
                    ";

                DataTable table = new DataTable();
                using (var con = new SqlConnection(ConfigurationManager.
                    ConnectionStrings["MovieAppDB"].ConnectionString))
                using (var cmd = new SqlCommand(query, con))
                using (var da = new SqlDataAdapter(cmd))
                {
                    cmd.CommandType = CommandType.Text;
                    da.Fill(table);
                }

                return "Added Successfully!!"; // If it successes it will post a success message
            }
            catch (Exception)
            {

                return "Failed to Add!!"; // If it fails it will post a fail message
            }
        }
        public string Put(Movies mad)
        {
            try
            {
                // This updates the data that already is in the system if needed. 
                //We tell the database wich values need to be changed with what values, we these values from the website
                string query = @"
                    update dbo.Movies set 
                    MovieTitle='" + mad.MovieTitle + @"'
                    ,Genre='" + mad.Genre + @"'
                    ,DateOfRelease='" + mad.DateOfRelease + @"'
                    ,PhotoFileName='" + mad.PhotoFileName + @"'
                    where MovieId=" + mad.MovieId + @"
                    ";

                DataTable table = new DataTable();
                using (var con = new SqlConnection(ConfigurationManager.
                    ConnectionStrings["MovieAppDB"].ConnectionString))
                using (var cmd = new SqlCommand(query, con))
                using (var da = new SqlDataAdapter(cmd))
                {
                    cmd.CommandType = CommandType.Text;
                    da.Fill(table);
                }

                return "Updated Successfully!!"; // If it successes it will post a success message
            }
            catch (Exception)
            {

                return "Failed to Update!!"; // If it fails it will post a fail message
            }
        }
        public string Delete(int Id)
        {
            try
            {
                // Here we check for the ID of the movie so if we wish to delete it
                string query = @"
                    delete from dbo.Movies 
                    where MovieId=" + Id + @"
                    ";

                DataTable table = new DataTable();
                using (var con = new SqlConnection(ConfigurationManager.
                    ConnectionStrings["MovieAppDB"].ConnectionString))
                using (var cmd = new SqlCommand(query, con))
                using (var da = new SqlDataAdapter(cmd))
                {
                    cmd.CommandType = CommandType.Text;
                    da.Fill(table);
                }

                return "Deleted Successfully!!"; // If it successes it will post a success message
            }
            catch (Exception)
            {

                return "Failed to Delete!!"; // If it fails it will post a fail message
            }
        }
        [Route("api/Movies/GetAllGenreNames")]
        [HttpGet]
        public HttpResponseMessage GetAllGenreNames()
        {
            string query = @"
                    select GenreName from dbo.Genre";

            DataTable table = new DataTable();
            using (var con = new SqlConnection(ConfigurationManager.
                ConnectionStrings["MovieAppDB"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }

            return Request.CreateResponse(HttpStatusCode.OK, table);
        }
        [Route("api/Movies/SaveFile")]
        public string SaveFile()
        {
            try
            {
                var httpRequest = HttpContext.Current.Request;
                var postedFile = httpRequest.Files[0];
                string filename = postedFile.FileName;
                var physicalPath = HttpContext.Current.Server.MapPath("~/Photos/" + filename);

                postedFile.SaveAs(physicalPath);

                return filename;
            }
            catch (Exception)
            {

                return "anonymous.png";
            }
        }

    }
}