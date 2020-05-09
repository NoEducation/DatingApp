using Dating.DTO;
using Dating.Models;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;


namespace Dating.Infrastrucutre
{
    public class UsersPdfGenerator : IUsersPdfGenerator
    {
        private readonly IDatingRepository _datingRepository;

        public UsersPdfGenerator(IDatingRepository datingRepository)
        {
            this._datingRepository = datingRepository;
        }

        /// <summary>
        /// I cannot find valid framework to serialzie html to be pdf, just for now i will leave it
        /// </summary>
        /// <returns></returns>
        public async Task<FileDto> GeneratePfdFile()
        {
            throw new System.NotImplementedException("Method html pdf serializtion not implemnt");

            //var users = await _datingRepository.GetUsers();
            //var basicHtml = GenerateBasicHtml(users);
            //SelectPdf.PdfDocument doc = new SelectPdf.PdfDocument();


            //doc.Save("test.pdf");
            //doc.Close();
            //using (var stream = new MemoryStream())
            //{
            //    pdf.Save(stream);
            //    return new FileDto("UserList.pdf", stream.ToArray());
            //}
        }


        private string GenerateBasicHtml(IEnumerable<UserModel> users)
        {
            var header1 = "<th>Username</th>";
            var header2 = "<th>Name</th>";
            var header3 = "<th>Surname</th>";
            var header4 = "<th>Email Address</th>";
            var headers = $"<tr>{header1}{header2}{header3}{header4}</tr>";
            var rows = new StringBuilder();
            foreach (var user in users)
            {
                var column1 = $"<td>{user.Name}</td>";
                var column2 = $"<td>{user.LookingFor}</td>";
                var column3 = $"<td>{user.City}</td>";
                var row = $"<tr>{column1}{column2}{column3}</tr>";
                rows.Append(row);
            }
            return $"<table>{headers}{rows.ToString()}</table>";
        }
    }
}
