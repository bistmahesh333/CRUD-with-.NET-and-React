import { GET_STUDENTS_REQUEST, GET_STUDENTS_SUCCESS, GET_STUDENTS_FAILURE } from './action';

const initialState = {
  students: [],
  loading: false,
  error: null,
};

const studentReducer = (state = initialState, action) => {
  switch (action.type) {
    case GET_STUDENTS_REQUEST:
      return { ...state, loading: true, error: null };
    case GET_STUDENTS_SUCCESS:
      return { ...state, loading: false, students: action.payload };
    case GET_STUDENTS_FAILURE:
      return { ...state, loading: false, error: action.payload };
    default:
      return state;
  }
};

export default studentReducer;
