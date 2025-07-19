import React, { useState, useEffect } from 'react';
import axios from 'axios';

function EditStudent() {
  const [students, setStudents] = useState([]);
      const [formData, setFormData] = useState({
    id: null,
    name: '',
    address: ''
  });
    
  const [previewData, setPreviewData] = useState(null);

  const apiBaseUrl = 'https://localhost:7077/api/Student';

  useEffect(() => {
    fetchStudents();
  }, []);

  const fetchStudents = async () => {
    try {
      const res = await axios.get(`${apiBaseUrl}/GetAllStudents`);
      setStudents(res.data);
    } catch (error) {
      console.error('Error fetching students:', error);
    }
  };
  //get ends here

  //edit start here

  const handleChange = (e) => {
    setFormData({ ...formData, [e.target.name]: e.target.value });
  };


  const handleSubmit = async (e) => {
    e.preventDefault();
    try {
      if (formData.id) {
        await axios.post(`${apiBaseUrl}/EditStudent`, formData);
        console.log('Student updated');
      } else {
        await axios.post(`${apiBaseUrl}/AddStudent`, formData);
        console.log('Student added');
      }
      fetchStudents();
      resetForm();
    } catch (error) {
      console.error('Error saving student:', error);
    }
  };

  const handleEdit = (student) => {
    setFormData({
      id: student.id,
      name: student.name,
      address: student.address
    });
  };

  const resetForm = () => {
    setFormData({ id: null, name: '', address: '' });
  };

  //preview
    const handlePreview = (student) => {
    setPreviewData(student);
  };

  return (
    <div>
      <h2>Student Form</h2>
      <form onSubmit={handleSubmit}>
        <input
          name="name"
          placeholder="Name"
          value={formData.name}
          onChange={handleChange}
        />
        <input
          name="address"
          placeholder="Address"
          value={formData.address}
          onChange={handleChange}
        />
        <button type="submit">{formData.id ? 'Update' : 'Add'} Student</button>
        <button type="button" onClick={resetForm}>Reset</button>
      </form>

      <h3>Student List</h3>
      <table border="1" cellPadding="8">
        <thead>
          <tr>
            <th>ID</th>
            <th>Name</th>
            <th>Address</th>
            <th>Actions</th>
          </tr>
        </thead>
        <tbody>
          {students.map((student) => (
            <tr key={student.id}>
              <td>{student.id}</td>
              <td>{student.name}</td>
              <td>{student.address}</td>
              <td>
                <button onClick={() => handleEdit(student)}>Edit</button>
                <button onClick={() => handlePreview(student)}>Preview</button>
              </td>
            </tr>
          ))}
        </tbody>
      </table>

      {previewData && (
        <div>
          <h3>Preview Student</h3>
          <p>ID: {previewData.id}</p>
          <p>Name: {previewData.name}</p>
          <p>Address: {previewData.address}</p>
        </div>
      )}

    </div>
  );
}
export default EditStudent;
