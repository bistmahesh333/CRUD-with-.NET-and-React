import { all } from 'redux-saga/effects';
import MemberReportSaga from './student/saga';

export default function* rootSaga() {
  yield all([MemberReportSaga()]);
}
