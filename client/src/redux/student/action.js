export const GET_STUDENTS_REQUEST = 'GET_STUDENTS_REQUEST';
export const GET_STUDENTS_SUCCESS = 'GET_STUDENTS_SUCCESS';
export const GET_STUDENTS_FAILURE = 'GET_STUDENTS_FAILURE';

export const getStudentsRequest = () => ({ type: GET_STUDENTS_REQUEST });
export const getStudentsSuccess = (students) => ({ type: GET_STUDENTS_SUCCESS, payload: students });
export const getStudentsFailure = (error) => ({ type: GET_STUDENTS_FAILURE, payload: error });
