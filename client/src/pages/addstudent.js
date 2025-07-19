import React, { useState } from 'react';
import axios from "axios";


function AddStudent(){
    const [StdData, setStdData] = useState({
        id : null,
        name : '',
        address : ''
});

  const apiBaseUrl = 'https://localhost:7077/api/Student';

  const handleChange = (e) => {
    setStdData({ ...StdData, [e.target.name]: e.target.value });
  };

      const resetForm = () => {
    setStdData({
        id : null,
        name : '',
        address : ''
    });
  };

    const handleSubmit = async (e) => {
        e.preventDefault();
    try {
      console.log('Submitting:', StdData);
      const res = await axios.post(`${apiBaseUrl}/AddStudent`, StdData);
      console.log('Save response:', res.data);
    } catch (err) {
      console.error('Error saving signup:', err);
    }
  };






  return(
        <div>
            <h2>Add Students</h2>
            <form onSubmit={handleSubmit}>
                <input name="name" placeholder="name" value={StdData.name} onChange={handleChange} />
                <input name="address" placeholder="address" value={StdData.address} onChange={handleChange} />                
                <button type="submit">Add</button>
                <button type="button" onClick={resetForm}>Reset</button>
            </form>
        </div>

  );


}
export default AddStudent;