using TovanyUchetV2.Data.Models;

public interface IDataService
{
    Task<List<Product>> GetAllProductsAsync();
    Task<Product?> GetProductByIdAsync(int id);
    Task AddOrUpdateProductAsync(Product product);
    Task DeleteProductAsync(int id);

    Task<List<Employee>> GetAllEmployeesAsync();
    Task<Employee?> GetEmployeeByIdAsync(int id);
    Task AddOrUpdateEmployeeAsync(Employee employee);

    Task<List<InventoryOperation>> GetAllInventoryOperationsAsync();
    Task AddInventoryOperationAsync(InventoryOperation operation);
    Task DeleteInventoryOperationAsync(int id);
    Task<InventoryOperation?> GetInventoryOperationByIdAsync(int id);
    Task UpdateInventoryOperationAsync(InventoryOperation operation);
}
