﻿namespace FashionClothesAndTrends.Application.DTOs;

public class UserDto
{
    public Guid Id { get; set; }
    public string Email { get; set; }
    public string Username { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string PhotoUrl { get; set; }
    public string Gender { get; set; }
    public int Age { get; set; }
    public DateTime Created { get; set; }
    public DateTime LastActive { get; set; }
    public List<UserPhotoDto> UserPhotos { get; set; }
    public string Token { get; set; }
}