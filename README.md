# 🏫 Student Management System

A web-based application designed to manage student profiles, enrollment, grades, and courses. This system allows students, professors, and administrators to interact with the platform based on their roles, offering specific functionalities such as viewing profiles, enrolling in courses, and managing academic data.

## Screenshots

### Login Page

![Login Page](/Student_Management/images/LoginPage.png)

### Signup Page

![Signup Page](/Student_Management/images/SignUpPage.png)

### Student Side

#### Profile Page

![Profile Page](/Student_Management/images/StudentProfilePage.png)

#### Enrollment Page

![Enrollment Page](/Student_Management/images/EnrollCoursePage.png)

#### After Enrollment

![After Enrollment Page](/Student_Management/images/AfterEnrollCourse.png)

### Professor Side

#### Home Page

![Professor Home Page](/Student_Management/images/ProfessorHomePage.png)

![Professor Home Page](/Student_Management/images/ProfessorHomePage2.png)

#### Edit Marks 

![Edit Marks](/Student_Management/images/EditMarks.png)

##### Validation

![Validation Page](/Student_Management/images/Validation.png)

### Admin DashBoard

![Admin Dashboard Page](/Student_Management/images/AdminHomePage.png)

#### Add Course Page

![Add Course Page](/Student_Management/images/AddCoursePage.png)

![Add Course Page](/Student_Management/images/AddCoursePage2.png)

## 🚀 Features

- **Student Login**: Students can log in to view their profile, enrolled courses, and grades.
- **Professor Login**: Professors can log in to manage courses and grades for their students.
- **Admin Dashboard**: Administrators can manage courses, add new courses, and manage student enrollment.
- **Dynamic Navbar**: The navigation bar changes based on the user role (Student, Professor, Admin).
- **Grade Management**: Students can view their obtained marks and grades in each course.
- **Role-Based Access Control**: Different access and features depending on the user role (Student, Professor, Admin).
- **Course Enrollment**: Students can enroll in courses from their profile page using a course enrollment form, and administrators can oversee the enrollment process.
- **Edit Marks Feature**: Professors can edit and update marks for individual students directly from their dashboard. Changes are reflected instantly in the student profile.
- **Automatic Grade Calculation**: The system automatically calculates and updates the final grade for a student based on their internal, external, and practical marks, following predefined grade criteria.


## 🛠️ Technologies Used

- **Frontend**: 
  - HTML
  - CSS (Bootstrap classes for styling)
  - ASP.NET Web Forms
- **Backend**: 
  - C# 
  - ASP.NET (Code-behind logic)
- **Database**: 
  - Microsoft SQL Server (Students , Professors , Admins , Courses, Grades , Enrollments)
- **Other**:
  - ADO.NET for database connectivity

## 📋 Project Structure

```bash
📁 Student_Management/
│
├── 📄 student_index.aspx         # Student profile and enrolled courses page
├── 📄 professor_index.aspx       # Professor dashboard
├── 📄 admin.aspx                 # Admin dashboard for managing courses
├── 📄 Login.aspx                 # Login page for all users
├── 📄 Enrollment.aspx            # Enrollment page for students
│
├── 📄 Navbar.ascx                # Navbar control for dynamic navigation based on user role
├── 📄 Navbar.ascx.cs             # Code-behind for handling navbar logic
│
├── 📁 App_Code/
│   └── 📄 DataAccess.cs          # Data access layer to interact with SQL Server database
│
├── 📁 App_Data/
│   └── 📄 dbConnectionString.config   # Database connection string
│
└── 📁 css/
    └── 📄 Styles.css             # Custom styling for the web pages
```

## ⚙️ Setup Instructions

1. **Clone the repository**:

   ```bash
   git clone https://github.com/Vipullakum007/Student_Management_System.git
   ```

2. **Open in Visual Studio**:

   - Open the solution file (`Student_Management_System.sln`) in Visual Studio.

3. **Database Configuration**:
   - Set up a SQL Server database with the necessary tables (`Students`, `Courses`, `Grades`, etc.).
   - Update the `dbConnectionString` in the `Web.config` file with your SQL Server credentials.

4. **Run the application**:
   - Press `F5` in Visual Studio to run the project.

## 🧑‍💻 User Roles & Access

- **Student**:
  - Can view their profile, enrolled courses, and grades.
  - Can enroll in new courses through the enrollment page.
  
- **Professor**:
  - Can manage course details and grades for students.
  
- **Admin**:
  - Can manage all courses, add new courses, and oversee student enrollment.


## 🏗️ Future Enhancements

- **Password Hashing**: Implement password hashing for security in the login process.
- **Responsive Design**: Improve the UI/UX to be more mobile-friendly.
- **Real-time Notifications**: Add notification support for students when new grades are uploaded.

## 🤝 Contributing

We welcome contributions! Feel free to submit issues or pull requests to improve the project.

### Steps to Contribute

1. Fork the repository.
2. Create a new branch (`git checkout -b feature-branch`).
3. Make your changes.
4. Submit a pull request.

## 📧 Contact

For any questions or inquiries, please contact the project maintainer at lakumvipul6351@gmail.com.

---

Enjoy managing students and courses with ease using **Student Management System**! 🎓
```
