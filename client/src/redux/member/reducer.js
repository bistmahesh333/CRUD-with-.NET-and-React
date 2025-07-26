import { actionTypes } from "./action";

var initialState = {

  FilterData: {
    memberTypeId: 0,
    memberId: 0,
  },

  MemberReport: [],
  MemberType: [],
};


const MemberReportReducer = (state = initialState, action) => {
  let updateState = Object.assign({}, state);
  let obj, FilterData;  

  switch (action.type) {
    case actionTypes.SET_MEMBER_TYPE:
      const MemberTypeData = action.data?.map((x)=>({
        key: x.memberTypeId,
        value: x.typeName,
      }));
      return {
        ...state,
        MemberType: MemberTypeData,
      };

     case actionTypes.SET_MEMBER_REPORT_FILTERS:
        obj = {};
        FilterData = Object.assign({}, state.Filterdata, action.data);
        return {
            ...state,
            FilterData: FilterData,
        };

       case actionTypes.RESET_MEMBER_REPORT_FILTERS:
          return {
                ...state,
          FilterData: initialState.FilterData,
          };


      case actionTypes.SET_MEMBER_REPORT:
        console.log("Reducer received data:", action.data);
        return {
          ...state,
          MemberReport: action.data,
        };

    default:
      return updateState;
  }
};    
export default MemberReportReducer;