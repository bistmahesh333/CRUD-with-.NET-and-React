import { actionTypes } from "./action";
const initialState = {
    MemberType: [],
};

export const reducer = (state = initialState, action) => {
  switch (action.type) {
    case actionTypes.SET_MEMBER_TYPE:
      return {
        ...state,
        memberTypes: action.data,
      };
    default:
      return state;
  }
};