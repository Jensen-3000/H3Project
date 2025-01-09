using H3Project.Data.Models;

namespace H3Project.Data.Utilities;

public class SeatGenerator
{
    public static List<Seat> GenerateTheaterSeats(int theaterId, int startId, int rows, int seatsPerRow)
    {
        var seats = new List<Seat>();
        var rowLetters = "ABCDEFGHIJKL".ToCharArray();
        var currentId = startId;

        for (var row = 0; row < rows; row++)
        for (var seatNum = 1; seatNum <= seatsPerRow; seatNum++)
            seats.Add(new Seat
            {
                Id = currentId++,
                Row = rowLetters[row].ToString(),
                Number = seatNum,
                IsAvailable = true,
                TheaterId = theaterId
            });

        return seats;
    }
}