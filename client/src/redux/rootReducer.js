import { combineReducers } from 'redux';
import studentReducer from './student/reducer';

const rootReducer = combineReducers({
  student: studentReducer,
});

export default rootReducer;
