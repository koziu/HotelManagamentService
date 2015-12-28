﻿using System.Data.Entity;
using HotelManagementService.Models;

namespace HotelManagementService.DAL.Context
{
  public class HotelManagementContext : DbContext
  {
    public HotelManagementContext()
      : base("HotelManagement")
    {
    }

    public DbSet<ClientModels> ClientModels { get; set; }
    public DbSet<Event> Events { get; set; }
    public DbSet<ReservationModels> ReservationModelses { get; set; }
    public DbSet<RoomModels> RoomModels { get; set; }
    public DbSet<RoomElementModels> RoomElementModelses { get; set; }
    public DbSet<EmployeeModels> EmployeeModels { get; set; }
    //public DbSet<ReservationEvents> ReservationEventse { get; set; } 
  }
}