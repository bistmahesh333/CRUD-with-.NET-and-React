import React, { useState, useEffect } from 'react';
import axios from 'axios';

function GetStudent() {
  const [students, setStudents] = useState([]);

  const apiBaseUrl = 'https://localhost:7077/api/Student';

  const fetchStudents = async () => {
    try {
      const res = await axios.get(`${apiBaseUrl}/GetAllStudents`);
      setStudents(res.data);
    } catch (error) {
      console.error('Error fetching students:', error);
    }
  };

  useEffect(() => {
    fetchStudents();
  }, []);

  return (
    <div>
      <h2>Student List</h2>
      <table border="1" cellPadding="8">
        <thead>
          <tr>
            <th>ID</th>
            <th>Name</th>
            <th>Address</th>
          </tr>
        </thead>
        <tbody>
          {students.map((student) => (
            <tr key={student.id}>
              <td>{student.id}</td>
              <td>{student.name}</td>
              <td>{student.address}</td>
            </tr>
          ))}
        </tbody>
      </table>
    </div>
  );
}
export default GetStudent;
