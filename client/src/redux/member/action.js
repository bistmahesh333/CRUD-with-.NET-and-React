export const actionTypes = {
    GET_MEMBER_TYPE: "GET_MEMBER_TYPE",
    SET_MEMBER_TYPE: "SET_MEMBER_TYPE",

};

export const MemberType = (data) => {
    return {
        type: actionTypes.SET_MEMBER_TYPE,
        data,
    };
};
