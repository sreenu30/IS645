﻿namespace IS645Project.Model
{
    public class Reservation
    {
        public int RoomNumber { get; set; }

        public int CustomerID { get; set; }

        public string CheckInDate {  get; set; }

        public string CheckOutDate { get; set; }
    }
}