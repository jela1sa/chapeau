using Chapeau.Models;
using Chapeau.ViewModels;
using Microsoft.Data.SqlClient;

namespace Chapeau.Repositories
{
    public class RekeningRepository : IRekeningRepository
    {
        private readonly string _connectionString;

        public RekeningRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("ChapeauDatabase");
        }


        //public RekeningRepository(string connectionString)
        //{
        //    _connectionString = connectionString;
        //}

        //THIS WAS EDITED ROBBE

        //public void VoegToe(Rekening rekening)
        //{
        //    rekening.Add(rekening);
        //    rekening.SaveChanges();
        //}

        //THIS WAS EDITED ROBBE
        public void VoegToe(Rekening rekening)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    string query = @"
                INSERT INTO Rekening
                (Bestelling_ID, TotaalBedrag, Fooi, BtwBedrag,
                 Betaalwijze, Feedback, Datum)
                VALUES
                (@Bestelling_ID, @TotaalBedrag, @Fooi, @BtwBedrag,
                 @Betaalwijze, @Feedback, @Datum)";

                    SqlCommand command = new SqlCommand(query, connection);

                    command.Parameters.AddWithValue("@Bestelling_ID", rekening.Bestelling_ID);
                    command.Parameters.AddWithValue("@TotaalBedrag", rekening.TotaalBedrag);
                    command.Parameters.AddWithValue("@Fooi", rekening.Fooi);
                    command.Parameters.AddWithValue("@BtwBedrag", rekening.BtwBedrag);
                    command.Parameters.AddWithValue("@Betaalwijze", rekening.Betaalwijze);
                    command.Parameters.AddWithValue("@Feedback", rekening.Feedback);
                    command.Parameters.AddWithValue("@Datum", rekening.Datum);

                    connection.Open();

                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected != 1)
                    {
                        throw new Exception("Insert failed");
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("SQL ERROR: " + ex.Message);
                throw;
            }
        }

        //public void WijzigTafelStatus(int bestellingId, string status)
        //{
        //    var bestelling = _context.Bestellingen
        //        .Include(b => b.Tafel)
        //        .FirstOrDefault(b => b.Bestelling_ID == bestellingId);

        //    if (bestelling != null)
        //    {
        //        bestelling.Tafel.Status = status;
        //        _context.SaveChanges();
        //    }
        //}



        //THIS WAS EDITED ROBBE
        public void WijzigTafelStatus(int bestellingId, string status)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                string query = @"
            UPDATE Tafel
            SET status = @status
            WHERE tafel_ID =
            (
                SELECT tafel_ID
                FROM Bestelling
                WHERE bestelling_ID = @bestellingId
            )";

                SqlCommand command = new SqlCommand(query, connection);

                command.Parameters.AddWithValue("@status", status);
                command.Parameters.AddWithValue("@bestellingId", bestellingId);

                int rowsAffected = command.ExecuteNonQuery();

                if (rowsAffected < 1)
                {
                    throw new Exception("No table status was updated.");
                }
            }
        }
    }

// Services/IRekeningService.cs
public interface IRekeningService
    {
        void VerwerkBetaling(RekeningViewModel model);
    }

    // Services/RekeningService.cs
    public class RekeningService : IRekeningService
    {
        private readonly IRekeningRepository _repository;
        private const decimal BTW_PERCENTAGE = 0.21m;

        public RekeningService(IRekeningRepository repository)
        {
            _repository = repository;
        }

        public void VerwerkBetaling(RekeningViewModel model)
        {
            decimal btw = model.TotaalBedrag - (model.TotaalBedrag / (1 + BTW_PERCENTAGE));

            var rekening = new Rekening
            {
                Bestelling_ID = model.Bestelling_ID,
                Rekening_ID = model.Bestelling_ID,
                TotaalBedrag = model.TotaalBedrag,
                Fooi = model.Fooi,
                BtwBedrag = btw,
                Betaalwijze = model.Betaalwijze,
                Feedback = model.Feedback,
                Datum = DateTime.Now
            };

            _repository.VoegToe(rekening);
            _repository.WijzigTafelStatus(model.Bestelling_ID, "vrij");
        }
    }
}
