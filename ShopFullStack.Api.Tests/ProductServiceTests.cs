using NSubstitute;
using ShopFullStack.Api.Dtos;
using ShopFullStack.Api.Interfaces;
using ShopFullStack.Api.Models;
using ShopFullStack.Api.Services;
using Xunit;

namespace ShopFullStack.Api.Tests;

public class ProductServiceTests
{
    private readonly IProductRepository _productRepository;
    private readonly ICategoryRepository _categoryRepository;
    private readonly ProductService _productService;

    public ProductServiceTests()
    {
        _productRepository = Substitute.For<IProductRepository>();
        _categoryRepository = Substitute.For<ICategoryRepository>();
        _productService = new ProductService(_productRepository, _categoryRepository);
    }

    [Fact]
    public async Task GetAllAsync_ReturnsAllProducts_WhenProductsExist()
    {
        // Arrange
        var category = new Category { Id = 1, Name = "Elektronika" };
        var products = new List<Product>
        {
            new Product { Id = 1, Name = "Laptop", Price = 999, Stock = 5, CategoryId = 1, Category = category }
        };
        _productRepository.GetAllWithCategoryAsync().Returns(products);

        // Act
        var result = await _productService.GetAllAsync();

        // Assert
        Assert.Single(result);
    }

    [Fact]
    public async Task GetByIdAsync_ReturnsProduct_WhenProductExists()
    {
        // Arrange
        var category = new Category { Id = 1, Name = "Elektronika" };
        var product = new Product { Id = 1, Name = "Laptop", Price = 999, Stock = 5, CategoryId = 1, Category = category };
        _productRepository.GetByIdWithCategoryAsync(1).Returns(product);

        // Act
        var result = await _productService.GetByIdAsync(1);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("Laptop", result.Name);
    }

    [Fact]
    public async Task GetByIdAsync_ReturnsNull_WhenProductDoesNotExist()
    {
        // Arrange
        _productRepository.GetByIdWithCategoryAsync(99).Returns((Product?)null);

        // Act
        var result = await _productService.GetByIdAsync(99);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task CreateAsync_ReturnsProductDto_WhenCategoryExists()
    {
        // Arrange
        var category = new Category { Id = 1, Name = "Elektronika" };
        var dto = new CreateProductDto { Name = "Telefon", Price = 500, Stock = 3, CategoryId = 1 };
        _categoryRepository.ExistsAsync(1).Returns(true);
        _categoryRepository.GetByIdAsync(1).Returns(category);

        // Act
        var result = await _productService.CreateAsync(dto);

        // Assert
        Assert.Equal("Telefon", result.Name);
        Assert.Equal("Elektronika", result.CategoryName);
    }

    [Fact]
    public async Task CreateAsync_ThrowsKeyNotFoundException_WhenCategoryDoesNotExist()
    {
        // Arrange
        var dto = new CreateProductDto { Name = "Telefon", Price = 500, Stock = 3, CategoryId = 99 };
        _categoryRepository.ExistsAsync(99).Returns(false);

        // Act & Assert
        await Assert.ThrowsAsync<KeyNotFoundException>(() => _productService.CreateAsync(dto));
    }

    [Fact]
    public async Task UpdateAsync_ReturnsTrue_WhenProductExists()
    {
        // Arrange
        var product = new Product { Id = 1, Name = "Laptop", Price = 999, Stock = 5, CategoryId = 1 };
        var dto = new CreateProductDto { Name = "Laptop Pro", Price = 1099, Stock = 4, CategoryId = 1 };
        _productRepository.GetByIdAsync(1).Returns(product);
        _categoryRepository.ExistsAsync(1).Returns(true);
        _productRepository.SaveChangesAsync().Returns(true);

        // Act
        var result = await _productService.UpdateAsync(1, dto);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public async Task UpdateAsync_ReturnsFalse_WhenProductDoesNotExist()
    {
        // Arrange
        var dto = new CreateProductDto { Name = "Laptop Pro", Price = 1099, Stock = 4, CategoryId = 1 };
        _productRepository.GetByIdAsync(99).Returns((Product?)null);

        // Act
        var result = await _productService.UpdateAsync(99, dto);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public async Task DeleteAsync_ReturnsTrue_WhenProductExists()
    {
        // Arrange
        var product = new Product { Id = 1, Name = "Laptop", Price = 999, Stock = 5, CategoryId = 1 };
        _productRepository.GetByIdAsync(1).Returns(product);
        _productRepository.SaveChangesAsync().Returns(true);

        // Act
        var result = await _productService.DeleteAsync(1);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public async Task DeleteAsync_ReturnsFalse_WhenProductDoesNotExist()
    {
        // Arrange
        _productRepository.GetByIdAsync(99).Returns((Product?)null);

        // Act
        var result = await _productService.DeleteAsync(99);

        // Assert
        Assert.False(result);
    }
}