import axios from 'axios';

const API_BASE_URL = 'https://localhost:7077/api/Student';

export const fetchStudentsApi = () => {
  return axios.get(`${API_BASE_URL}/GetAllStudents`);
};
