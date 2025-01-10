using H3Project.Data.Models;

namespace H3Project.Data.Utilities;

public class SeatCreator
{
    //public static List<Seat> GenerateTheaterSeats(int theaterId, int startId, int rows, int seatsPerRow)
    //{
    //    var seats = new List<Seat>();
    //    var rowLetters = "ABCDEFGHIJKL".ToCharArray();
    //    var currentId = startId;

    //    for (var row = 0; row < rows; row++)
    //    for (var seatNum = 1; seatNum <= seatsPerRow; seatNum++)
    //        seats.Add(new Seat
    //        {
    //            Id = currentId++,
    //            Row = rowLetters[row].ToString(),
    //            Number = seatNum,
    //            IsAvailable = true,
    //            TheaterId = theaterId
    //        });

    //    return seats;
    //}

   public static List<SeatModel> CreateSeats(int screenId, string[] rows, int seatsPerRow, int startingSeatId)
    {
        var seats = new List<SeatModel>();
        var currentId = startingSeatId;

        foreach (var row in rows)
        {
            for (var number = 1; number <= seatsPerRow; number++)
            {
                seats.Add(new SeatModel
                {
                    Id = currentId++,
                    Row = row,
                    Number = number,
                    ScreenId = screenId
                });
            }
        }

        return seats;
    }
}