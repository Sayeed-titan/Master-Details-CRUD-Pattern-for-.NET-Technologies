erDiagram
    EMPLOYEE ||--o{ EMPLOYEE_SKILL : has
    EMPLOYEE_SKILL }o--|| SKILL : references
    EMPLOYEE {
        int Id PK
        string Name
        int Age
        byte[] Photo
        bool IsActive
        datetime HireDate
        decimal Salary
        int Type
    }
    EMPLOYEE_SKILL {
        int EmployeeId PK,FK
        int SkillId PK,FK
        int ProficiencyLevel
    }
    SKILL {
        int Id PK
        string Name
        string Description
    }
