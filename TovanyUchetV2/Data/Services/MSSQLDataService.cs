using Microsoft.EntityFrameworkCore;
using TovanyUchetV2.Data;
using TovanyUchetV2.Data.Models;

namespace TovanyUchetV2.Services
{
    public class MSSQLDataService : IDataService
    {
        private readonly ApplicationDbContext _db;

        public MSSQLDataService(ApplicationDbContext db)
        {
            _db = db;
        }

        public MSSQLDataService()
        {
        }

        public async Task<List<InventoryOperation>> GetAllInventoryOperationsAsync()
        {
            return await _db.InventoryOperations
                .Include(op => op.Product)
                .Include(op => op.Employee)
                .OrderByDescending(op => op.Date)
                .ToListAsync();
        }

        public async Task<List<Product>> GetAllProductsAsync()
        {
            return await _db.Products.ToListAsync();
        }

        public async Task<Product?> GetProductByIdAsync(int id)
        {
            return await _db.Products.FindAsync(id);
        }

        public async Task AddOrUpdateProductAsync(Product product)
        {
            if (product.Id == 0)
                _db.Products.Add(product);
            else
                _db.Products.Update(product);

            await _db.SaveChangesAsync();
        }

        public async Task DeleteProductAsync(int id)
        {
            var product = await _db.Products.FindAsync(id);
            if (product != null)
            {
                _db.Products.Remove(product);
                await _db.SaveChangesAsync();
            }
        }

        public async Task<List<Employee>> GetAllEmployeesAsync()
        {
            return await _db.Employees.ToListAsync(); // Добавлено получение сотрудников
        }

        public async Task AddInventoryOperationAsync(InventoryOperation operation)
        {
            // Проверка, что товар существует
            var product = await _db.Products.FindAsync(operation.ProductId);
            if (product == null)
                throw new ArgumentException("Товар не найден");

            _db.InventoryOperations.Add(operation);

            // Аудит
            _db.AuditLogs.Add(new AuditLog
            {
                Action = "Добавление",
                Entity = $"Operation (ProductId: {operation.ProductId}, Qty: {operation.Quantity})",
                Date = DateTime.Now,
                User = "admin" // если авторизация, бери из контекста
            });

            _db.InventoryOperations.Add(operation);
            await _db.SaveChangesAsync();
        }

        public async Task<Employee?> GetEmployeeByIdAsync(int id)
        {
            return await _db.Employees.FindAsync(id);
        }

        public async Task AddOrUpdateEmployeeAsync(Employee employee)

        {
            if (employee.Id == 0)
            {
                _db.Employees.Add(employee);
            }
            else
            {
                _db.Employees.Update(employee);
            }

            await _db.SaveChangesAsync();
        }

        public async Task DeleteInventoryOperationAsync(int id)
        {
            var op = await _db.InventoryOperations.FindAsync(id);
            if (op != null)
            {
                _db.InventoryOperations.Remove(op);
                await _db.SaveChangesAsync();
            }
        }

        public async Task<InventoryOperation?> GetInventoryOperationByIdAsync(int id)
        {
            return await _db.InventoryOperations
                .Include(o => o.Product)
                .Include(o => o.Employee)
                .FirstOrDefaultAsync(o => o.Id == id);
        }

        public async Task UpdateInventoryOperationAsync(InventoryOperation operation)
        {
            _db.InventoryOperations.Update(operation);
            await _db.SaveChangesAsync();
        }


    }
}