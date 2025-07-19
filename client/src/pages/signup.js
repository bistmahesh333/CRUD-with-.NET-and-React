import React, { useState, useEffect } from 'react';
import axios from 'axios';

function SignupCrud() {
  const [signups, setSignups] = useState([]);
  const [formData, setFormData] = useState({
    id: null,
    username: '',
    password: '',
    fullname: '',
    dob: '',
    email: ''
  });
  const [isEditing, setIsEditing] = useState(false);

  const apiBaseUrl = 'https://localhost:7077/api';

  useEffect(() => {
    fetchSignups();
  }, []);

  const fetchSignups = async () => {
    try {
      const res = await axios.get(`${apiBaseUrl}/Signup/GetSignup`, { params: { id: null } });
      console.log("Fetched:", res.data);
      setSignups(res.data);
    } catch (err) {
      console.error('Error fetching signups:', err);
    }
  };

  const handleChange = (e) => {
    setFormData({ ...formData, [e.target.name]: e.target.value });
  };

  const handleSubmit = async (e) => {
    e.preventDefault();

    try {
      console.log('Submitting:', formData);
      const res = await axios.post(`${apiBaseUrl}/Signup/AddEditSignup`, formData);
      console.log('Save response:', res.data);

      fetchSignups();
      resetForm();
    } catch (err) {
      console.error('Error saving signup:', err);
    }
  };

  const handleEdit = (signup) => {
    setFormData(signup);
    setIsEditing(true);
  };

  const resetForm = () => {
    setFormData({
      id: null,
      username: '',
      password: '',
      fullname: '',
      dob: '',
      email: ''
    });
    setIsEditing(false);
  };

  return (
    <div>
      <h2>{isEditing ? 'Edit Signup' : 'Add Signup'}</h2>
      <form onSubmit={handleSubmit}>
        <input name="username" placeholder="Username" value={formData.username} onChange={handleChange} required />
        <input name="password" placeholder="Password" value={formData.password} onChange={handleChange} required />
        <input name="fullname" placeholder="Full Name" value={formData.fullname} onChange={handleChange} required />
        <input name="dob" placeholder="DOB (yyyy-mm-dd)" value={formData.dob} onChange={handleChange} required />
        <input name="email" placeholder="Email" value={formData.email} onChange={handleChange} required />
        
        <button type="submit">{isEditing ? 'Update' : 'Add'}</button>
        <button type="button" onClick={resetForm}>Reset</button>
      </form>

      <h3>Signup List</h3>
      <table border="1" cellPadding="5">
        <thead>
          <tr>
            <th>Id</th>
            <th>Username</th>
            <th>Fullname</th>
            <th>DOB</th>
            <th>Email</th>
            <th>Actions</th>
          </tr>
        </thead>
        <tbody>
          {signups.map((signup) => (
            <tr key={signup.id}>
              <td>{signup.id}</td>
              <td>{signup.username}</td>
              <td>{signup.fullname}</td>
              <td>{signup.dob}</td>
              <td>{signup.email}</td>
              <td>
                <button onClick={() => handleEdit(signup)}>Edit</button>
              </td>
            </tr>
          ))}
        </tbody>
      </table>
    </div>
  );
}

export default SignupCrud;
