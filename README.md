# Employee Management System

## Project Documentation

---

### 1. **Project Overview**
This project implements an Employee Management System with the following key features:
- **Employee CRUD Operations**
- **Department Management**
- **Employee Performance Review**
- **Complex Query Optimization**

The application consists of a backend developed using **ASP.NET Core** and a frontend built with **React.js**. A **SQL Server** database is used to store and manage the data.

---

### 2. **Features**
#### 2.1 Employee CRUD Operations
- **Create**: Add a new employee with details such as name, email, phone, department, position, and joining date.
- **Read**: List all employees with pagination.
- **Update**: Edit employee details.
- **Delete**: Soft delete employees using a "Deleted" flag.

#### 2.2 Department Management
- Manage departments with fields like department name, manager (one-to-one relationship with employees), and budget.
- Assign employees to departments with a foreign key relationship.

#### 2.3 Employee Performance Review
- Store performance reviews in a separate table with fields for review date, review score, and review notes.
- Support one-to-many relationships between employees and their performance reviews.

#### Complex Query Optimization
- Calculate the average performance score for each department.
- Exclude employees without reviews.
- Optimize query performance using indexes and joins.

#### Search and Filter
- Paginate data for efficient loading.

---

### **Database Design**

#### Tables

**Employee Table**:
| Field           | Type         | Description                          |
|-----------------|--------------|--------------------------------------|
| EmployeeID      | INT          | Primary Key                         |
| Name            | VARCHAR(255) | Employee name                       |
| Email           | VARCHAR(255) | Employee email                      |
| Phone           | VARCHAR(15)  | Employee phone number               |
| Position        | VARCHAR(255) | Employee position                   |
| JoinDate        | DATE         | Employee joining date               |
| DepartmentID    | INT          | Foreign Key to Department Table     |
| Status          | BIT          | Active/Inactive or Deleted flag     |

**Department Table**:
| Field           | Type         | Description                          |
|-----------------|--------------|--------------------------------------|
| DepartmentID    | INT          | Primary Key                         |
| DepartmentName  | VARCHAR(255) | Name of the department              |
| ManagerID       | INT          | Foreign Key to Employee Table       |
| Budget          | DECIMAL(18,2)| Department budget                   |

**PerformanceReview Table**:
| Field           | Type         | Description                          |
|-----------------|--------------|--------------------------------------|
| ReviewID        | INT          | Primary Key                         |
| EmployeeID      | INT          | Foreign Key to Employee Table       |
| ReviewDate      | DATE         | Date of the performance review      |
| ReviewScore     | TINYINT      | Score (1-10)                        |
| ReviewNotes     | TEXT         | Notes from the review               |

#### Relationships
- **Employee** → **Department**: One-to-Many
- **Employee** → **PerformanceReview**: One-to-Many

#### Constraints
- Foreign key constraints to ensure data integrity.
- Soft delete implementation using the `Status` field.

---


**Complex Query**:
- `GET /api/departments/average-scores`: Calculate the average performance score per department.

---

- **Employee Management**:
  - View, add, update, and delete employees.
- **Performance Display**:
  - Display average performance scores per department.

---

- Install **.NET Core SDK**
- Install **SQL Server**

1. Clone the repository.
2. Navigate to the backend folder.
3. Run `dotnet restore`.
4. Update the `appsettings.json` file with your database connection string.
5. Run `dotnet ef database update` to create the database.
6. Start the application with `dotnet run`.

---

- Used pagination for Employee list endpoints.
- Optimized SQL queries with joins.

---

1. **Source Code**:
   - ASP.NET MVC5
2. **Database Scripts**:
   - SQL scripts for table creation, indexes, and stored procedures.
3. **Documentation**:
   - README with setup instructions and feature overview.
4. **Unit Tests**:
   - Basic unit tests for CRUD operations.



# EmpMng
# EmpMng
