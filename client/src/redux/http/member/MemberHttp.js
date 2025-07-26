
import axios from "axios";

export const httpGetMemberType = () =>
  axios.get("https://localhost:7077/api/MemberType/GetMemberTypes");

export const httpGetMemberReport = (memberId = 0, memberTypeId = 0) =>
  axios.get("https://localhost:7077/api/MemberType/GetMembers", {
    params: {
      memberId,
      memberTypeId,
    },
  });











// import axios from "axios";
// export const httpGetMemberType = () => axios.get("https://localhost:7077/api/MemberType/GetMemberTypes");
// export const httpGetMemberReport = () => axios.get("https://localhost:7077/api/MemberType/GetMembers");
