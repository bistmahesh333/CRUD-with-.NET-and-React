export const actionTypes = {
    GET_MEMBER_TYPE: "GET_MEMBER_TYPE",
    SET_MEMBER_TYPE: "SET_MEMBER_TYPE",

    SET_MEMBER_REPORT_FILTERS: "SET_MEMBER_REPORT_FILTERS",
	RESET_MEMBER_REPORT_FILTERS: "RESET_MEMBER_REPORT_FILTERS",

    GET_MEMBER_REPORT: "GET_MEMBER_REPORT",
    SET_MEMBER_REPORT: "SET_MEMBER_REPORT",
};


// for member type
export const SetMemberType = (data) => {
    return {
        type: actionTypes.SET_MEMBER_TYPE,
        data,
    };
};


export const GetMemberType = (data) => {
    return {
        type: actionTypes.GET_MEMBER_TYPE,
        data,
    };
};

//filter
export const SetMemberReportFilter = (data) => {
	return {
		type: actionTypes.SET_MEMBER_REPORT_FILTERS,
		data,
	};
};

export const ResetMemberReportFilter = (data) => {
	return {
		type: actionTypes.RESET_MEMBER_REPORT_FILTERS,
		data,
	};
};

//for member report

export const GetMemberReport = (data) => {
    
    return {
        type: actionTypes.GET_MEMBER_REPORT,
        data,
    };
};
