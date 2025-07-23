import React, { useEffect } from 'react';
import { useDispatch, useSelector } from 'react-redux';
import { getStudentsRequest } from '../../redux/student/action';

function GetStudent() {
  const dispatch = useDispatch();
  const { students, loading, error } = useSelector(state => state.student);

  useEffect(() => {
    dispatch(getStudentsRequest());
  }, [dispatch]);

  return (
    <div>
      <h2>Student List</h2>
      {loading && <p>Loading...</p>}
      {error && <p style={{ color: 'red' }}>{error}</p>}
      <table border="1" cellPadding="8">
        <thead>
          <tr>
            <th>ID</th>
            <th>Name</th>
            <th>Address</th>
          </tr>
        </thead>
        <tbody>
          {students.map(student => (
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
