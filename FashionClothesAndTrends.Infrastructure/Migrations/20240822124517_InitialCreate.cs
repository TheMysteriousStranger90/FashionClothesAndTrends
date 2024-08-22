using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace FashionClothesAndTrends.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Gender = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastActive = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Address_AddressLine = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Address_City = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Address_State = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Address_PostalCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Address_Country = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Address_IsDefault = table.Column<bool>(type: "bit", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ClothingBrands",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClothingBrands", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Coupon",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DiscountPercentage = table.Column<int>(type: "int", nullable: false),
                    ExpiryDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Coupon", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DeliveryMethods",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ShortName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DeliveryTime = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeliveryMethods", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Notifications",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsRead = table.Column<bool>(type: "bit", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notifications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Notifications_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderHistories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OrderDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TotalAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ShippingAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderHistories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderHistories_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserPhotos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsMain = table.Column<bool>(type: "bit", nullable: false),
                    PublicId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserPhotos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserPhotos_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Wishlists",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Wishlists", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Wishlists_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ClothingItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Gender = table.Column<int>(type: "int", nullable: false),
                    Size = table.Column<int>(type: "int", nullable: false),
                    Category = table.Column<int>(type: "int", nullable: false),
                    Discount = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    IsInStock = table.Column<bool>(type: "bit", nullable: false),
                    ClothingBrandId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClothingItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClothingItems_ClothingBrands_ClothingBrandId",
                        column: x => x.ClothingBrandId,
                        principalTable: "ClothingBrands",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BuyerEmail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OrderDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    ShipToAddress_FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ShipToAddress_LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ShipToAddress_Country = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ShipToAddress_City = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ShipToAddress_State = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ShipToAddress_AddressLine = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ShipToAddress_PostalCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DeliveryMethodId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CouponId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Subtotal = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PaymentIntentId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orders_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Orders_Coupon_CouponId",
                        column: x => x.CouponId,
                        principalTable: "Coupon",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Orders_DeliveryMethods_DeliveryMethodId",
                        column: x => x.DeliveryMethodId,
                        principalTable: "DeliveryMethods",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "OrderItemHistories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OrderHistoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClothingItemId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClothingItemName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    PriceAtPurchase = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderItemHistories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderItemHistories_OrderHistories_OrderHistoryId",
                        column: x => x.OrderHistoryId,
                        principalTable: "OrderHistories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ClothingItemPhotos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsMain = table.Column<bool>(type: "bit", nullable: false),
                    PublicId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ClothingItemId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClothingItemPhotos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClothingItemPhotos_ClothingItems_ClothingItemId",
                        column: x => x.ClothingItemId,
                        principalTable: "ClothingItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Comments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClothingItemId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Comments_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Comments_ClothingItems_ClothingItemId",
                        column: x => x.ClothingItemId,
                        principalTable: "ClothingItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FavoriteItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClothingItemId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FavoriteItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FavoriteItems_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FavoriteItems_ClothingItems_ClothingItemId",
                        column: x => x.ClothingItemId,
                        principalTable: "ClothingItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Ratings",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Score = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClothingItemId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ratings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ratings_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Ratings_ClothingItems_ClothingItemId",
                        column: x => x.ClothingItemId,
                        principalTable: "ClothingItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WishlistItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    WishlistId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClothingItemId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WishlistItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WishlistItems_ClothingItems_ClothingItemId",
                        column: x => x.ClothingItemId,
                        principalTable: "ClothingItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WishlistItems_Wishlists_WishlistId",
                        column: x => x.WishlistId,
                        principalTable: "Wishlists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ItemOrdered_ClothingItemId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ItemOrdered_ClothingItemName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    OrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderItems_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LikesDislikes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsLike = table.Column<bool>(type: "bit", nullable: false),
                    CommentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LikesDislikes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LikesDislikes_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LikesDislikes_Comments_CommentId",
                        column: x => x.CommentId,
                        principalTable: "Comments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "ClothingBrands",
                columns: new[] { "Id", "CreatedAt", "Description", "LastUpdatedAt", "Name" },
                values: new object[,]
                {
                    { new Guid("3d6f79a2-c462-4c28-ae5f-0ec93b7f4e01"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Luxury fashion brand from France", null, "Chanel" },
                    { new Guid("5d24a48b-6c72-4e2a-9ef2-64d0f657bfc6"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Italian luxury fashion company", null, "Versace" },
                    { new Guid("a2c5c305-f2c2-45e7-8f7d-c489bb7f7e8a"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Italian luxury fashion house", null, "Prada" },
                    { new Guid("b5d6b8f8-dad4-4f2f-8c52-2911d856b3ad"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "High-end French fashion house", null, "Louis Vuitton" },
                    { new Guid("c981db82-b2f1-48c3-9864-efc6c56a5b0e"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Italian luxury fashion brand", null, "Gucci" },
                    { new Guid("e96c60b6-09df-4e1a-9d6c-617bdd48eaf5"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "French luxury fashion house", null, "Dior" }
                });

            migrationBuilder.InsertData(
                table: "DeliveryMethods",
                columns: new[] { "Id", "CreatedAt", "DeliveryTime", "Description", "LastUpdatedAt", "Price", "ShortName" },
                values: new object[,]
                {
                    { new Guid("14e6ee7b-fab1-4045-8b22-cd9785e3fd5e"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "2-5 Days", "Get it within 5 days", null, 5m, "UPS2" },
                    { new Guid("20de984f-88af-40ba-bf03-8f0163fd507c"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "1-2 Days", "Fastest delivery time", null, 10m, "UPS1" },
                    { new Guid("44b01398-285b-4b2e-8ba5-05d6028a1ea3"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "5-10 Days", "Slower but cheap", null, 2m, "UPS3" },
                    { new Guid("8b0e5ff3-5b6d-4528-8e1a-8d869b95fc5c"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "1-2 Weeks", "Free! You get what you pay for", null, 0m, "FREE" }
                });

            migrationBuilder.InsertData(
                table: "ClothingItems",
                columns: new[] { "Id", "Category", "ClothingBrandId", "CreatedAt", "Description", "Discount", "Gender", "IsInStock", "LastUpdatedAt", "Name", "Price", "Size" },
                values: new object[,]
                {
                    { new Guid("9eaf2bd3-7441-4533-bd25-82e55ba34b90"), 2, new Guid("3d6f79a2-c462-4c28-ae5f-0ec93b7f4e01"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Classic Chanel tweed jacket in black.", null, 1, true, null, "Chanel JACKET", 5000.00m, 2 },
                    { new Guid("b3ad6ec9-3a54-48f6-b4f9-ca7bae3318dd"), 3, new Guid("c981db82-b2f1-48c3-9864-efc6c56a5b0e"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "The GG Marmont belt continues to enrich each new collection with its streamlined design. Inspired by an archival design from the 1970s, the line's monogram Double G hardware is presented in a shiny silver tone atop this black leather variation.", null, 0, true, null, "Gucci GG MARMONT THIN BELT", 450.00m, 3 },
                    { new Guid("b7a5a95c-9e43-4c58-8b9a-ccac22bd2b70"), 4, new Guid("e96c60b6-09df-4e1a-9d6c-617bdd48eaf5"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "New for Winter 2024, the Dior Icon heeled ankle boot transcends House codes of couture refinement. The black suede calfskin upper is elevated by elastic bands on the sides and the gold-finish metal CD signature on the back. The 8-cm (3) Graphic Cannage cylindrical heel in gold-finish metal offers a modern 3D version of the House's iconic motif. Featuring a square toe, the sophisticated and comfortable ankle boot will add the finishing touch to any of the season's looks.", null, 1, true, null, "Dior Dior Icon Heeled Ankle Boot", 2900.00m, 2 },
                    { new Guid("cc193c80-e369-4e6e-9f57-0514cc47a8ef"), 0, new Guid("5d24a48b-6c72-4e2a-9ef2-64d0f657bfc6"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "A regular-fit, long-sleeved fluid shirt featuring an all-over tonal Barocco devore motif.", null, 0, true, null, "Versace Barocco Devore Shirt", 1200.00m, 3 },
                    { new Guid("cec7ead7-fb73-4bc5-83f9-bf2a038682b9"), 3, new Guid("b5d6b8f8-dad4-4f2f-8c52-2911d856b3ad"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "The LV Gram Square Cat Eye sunglasses feature a distinctive signature from Louis Vuitton’s jewelry and belts collections. The slim acetate and metal temples are adorned with the LV Initials and two Monogram Flowers finely crafted in gold-tone metal. Monogram Flower details on the lenses and end tips add an extra House touch. These stylish, feminine sunglasses are ideal for accenting a summer outfit.", null, 1, true, null, "LV Gram Square Cat Eye Sunglasses", 3200.00m, 2 },
                    { new Guid("ff62aac9-6985-4a40-affc-a89190d136a2"), 0, new Guid("a2c5c305-f2c2-45e7-8f7d-c489bb7f7e8a"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "An essential item of the brand, the Prada jersey T-shirt embodies the luxury of simplicity that becomes an attitude and search to reinvent the bases and propose new meanings. The design is accented with the brand's emblematic lettering logo presented here in a silicone version.", null, 0, true, null, "Prada Cotton T-shirt", 950.00m, 4 }
                });

            migrationBuilder.InsertData(
                table: "ClothingItemPhotos",
                columns: new[] { "Id", "ClothingItemId", "CreatedAt", "IsMain", "LastUpdatedAt", "PublicId", "Url" },
                values: new object[,]
                {
                    { new Guid("062bc308-0dec-48f5-bbcd-8432b72ec85f"), new Guid("cec7ead7-fb73-4bc5-83f9-bf2a038682b9"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, null, "PublicId5", "https://eu.louisvuitton.com/images/is/image/lv/1/PP_VP_L/louis-vuitton-lv-gram-square-cat-eye-sunglasses-s00-sunglasses--Z2459U_PM2_Front%20view.png?wid=1090&hei=1090" },
                    { new Guid("4af611af-827e-4146-9fae-7f2d719920bb"), new Guid("ff62aac9-6985-4a40-affc-a89190d136a2"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, null, "PublicId2", "https://www.prada.com/content/dam/pradabkg_products/U/UJN/UJN815/1052F0002/UJN815_1052_F0002_S_221_SLF.jpg/_jcr_content/renditions/cq5dam.web.hebebed.1000.1000.jpg" },
                    { new Guid("7a06938b-c7e5-4ff9-9bb4-27ffb7c5b477"), new Guid("9eaf2bd3-7441-4533-bd25-82e55ba34b90"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, null, "PublicId6", "https://www.chanel.com/images//t_zoomportee/f_auto//jacket-black-lambskin-lambskin-packshot-alternative-p78125c7009094305-9548808159262.jpg" },
                    { new Guid("7cb83a8a-8076-4dfb-b4bf-a5e7e47de44b"), new Guid("b7a5a95c-9e43-4c58-8b9a-ccac22bd2b70"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, null, "PublicId4", "https://www.dior.com/couture/ecommerce/media/catalog/product/Q/K/1721839565_KCT067VVV_S900_E03_GHC.jpg?imwidth=720" },
                    { new Guid("c900e8a8-c0a8-40a8-94e3-077e9d0f102b"), new Guid("cc193c80-e369-4e6e-9f57-0514cc47a8ef"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, null, "PublicId1", "https://www.versace.com/dw/image/v2/BGWN_PRD/on/demandware.static/-/Sites-ver-master-catalog/default/dwf9d0b70e/original/90_1012141-1A11358_1B000_10_BaroccoDevorShirt-Shirts-Versace-online-store_0_2.jpg?sw=1200&q=85&strip=true" },
                    { new Guid("d1f2b65c-1f7c-4318-960b-1b6583b490b8"), new Guid("b3ad6ec9-3a54-48f6-b4f9-ca7bae3318dd"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, null, "PublicId3", "https://media.gucci.com/style/DarkGray_Center_0_0_2400x2400/1714409103/414516_0AABG_1000_001_100_0000_Light-GG-Marmont-thin-belt.jpg" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_ClothingItemPhotos_ClothingItemId",
                table: "ClothingItemPhotos",
                column: "ClothingItemId");

            migrationBuilder.CreateIndex(
                name: "IX_ClothingItems_ClothingBrandId",
                table: "ClothingItems",
                column: "ClothingBrandId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_ClothingItemId",
                table: "Comments",
                column: "ClothingItemId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_UserId",
                table: "Comments",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_FavoriteItems_ClothingItemId",
                table: "FavoriteItems",
                column: "ClothingItemId");

            migrationBuilder.CreateIndex(
                name: "IX_FavoriteItems_UserId",
                table: "FavoriteItems",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_LikesDislikes_CommentId",
                table: "LikesDislikes",
                column: "CommentId");

            migrationBuilder.CreateIndex(
                name: "IX_LikesDislikes_UserId",
                table: "LikesDislikes",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_UserId",
                table: "Notifications",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderHistories_UserId",
                table: "OrderHistories",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItemHistories_OrderHistoryId",
                table: "OrderItemHistories",
                column: "OrderHistoryId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_OrderId",
                table: "OrderItems",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_CouponId",
                table: "Orders",
                column: "CouponId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_DeliveryMethodId",
                table: "Orders",
                column: "DeliveryMethodId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_UserId",
                table: "Orders",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Ratings_ClothingItemId",
                table: "Ratings",
                column: "ClothingItemId");

            migrationBuilder.CreateIndex(
                name: "IX_Ratings_UserId",
                table: "Ratings",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserPhotos_UserId",
                table: "UserPhotos",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_WishlistItems_ClothingItemId",
                table: "WishlistItems",
                column: "ClothingItemId");

            migrationBuilder.CreateIndex(
                name: "IX_WishlistItems_WishlistId",
                table: "WishlistItems",
                column: "WishlistId");

            migrationBuilder.CreateIndex(
                name: "IX_Wishlists_UserId",
                table: "Wishlists",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "ClothingItemPhotos");

            migrationBuilder.DropTable(
                name: "FavoriteItems");

            migrationBuilder.DropTable(
                name: "LikesDislikes");

            migrationBuilder.DropTable(
                name: "Notifications");

            migrationBuilder.DropTable(
                name: "OrderItemHistories");

            migrationBuilder.DropTable(
                name: "OrderItems");

            migrationBuilder.DropTable(
                name: "Ratings");

            migrationBuilder.DropTable(
                name: "UserPhotos");

            migrationBuilder.DropTable(
                name: "WishlistItems");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Comments");

            migrationBuilder.DropTable(
                name: "OrderHistories");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Wishlists");

            migrationBuilder.DropTable(
                name: "ClothingItems");

            migrationBuilder.DropTable(
                name: "Coupon");

            migrationBuilder.DropTable(
                name: "DeliveryMethods");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "ClothingBrands");
        }
    }
}
