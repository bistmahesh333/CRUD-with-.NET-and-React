import { combineReducers } from 'redux';
import MemberReportReducer from './student/reducer';

const rootReducer = combineReducers({
  MemberReportReducer: MemberReportReducer,
});

export default rootReducer;
