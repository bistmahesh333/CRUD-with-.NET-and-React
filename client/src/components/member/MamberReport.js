import React, { useEffect } from "react";
import { shallowEqual, useDispatch, useSelector } from "react-redux";
import {
  GetMemberType,
  GetMemberReport,
  SetMemberReportFilter,
  ResetMemberReportFilter
} from "../../redux/member/action";

function MemberReport() {
  const dispatch = useDispatch();

  // Redux state
  const MemberReport = useSelector((state) => state.MemberReportReducer.MemberReport, shallowEqual);
  const MemberType = useSelector((state) => state.MemberReportReducer.MemberType, shallowEqual);
  const FilterData = useSelector((state) => state.MemberReportReducer.FilterData, shallowEqual) || {};


  useEffect(() => {
    dispatch(GetMemberType());
  }, [dispatch]);

    useEffect(() => {
    dispatch(ResetMemberReportFilter());
  }, [dispatch]);

  const handleSearch = () => {
      FilterData.memberTypeId = FilterData.memberTypeId  === "" ? 0 : FilterData.memberTypeId;
      FilterData.memberId = FilterData.memberId === "" ? 0 : FilterData.memberId;
    dispatch(GetMemberReport(FilterData));
  };



  const handleOnChange = (name, value) => {
    let keyValue = {};
    keyValue[name] = value;
    dispatch(SetMemberReportFilter(keyValue));
  };

  return (
    <div>
      <h2>Member Report</h2>

      {/* ComboBox for Member Type */}
      <div>
        <label>Member Type:</label>
       <select
        id="MemberType"
        value={FilterData.memberTypeId || ""}
        onChange={(e) => handleOnChange("memberTypeId", e.target.value)}
      >
        <option value="">-- Select Member Type --</option>
        {MemberType &&
          MemberType.map((type) => (
            <option key={type.memberTypeId} value={type.memberTypeId}>
              {type.memberTypeName}
            </option>
          ))} 
      </select>
      </div>

      {/* Search Button */}
      <div style={{ marginTop: "10px" }}>
        <button onClick={handleSearch}>Search</button>
      </div>

      {/* Table to Display Member Report */}
      <div style={{ marginTop: "20px" }}>
        <table border="1" cellPadding="5">
          <thead>
            <tr>
              <th>Member ID</th>
              <th>Member Name</th>
              <th>Address Name</th>
              <th>Member Type</th>
              <th>Email</th>
              <th>Phone</th>
              <th>Gender</th>
              <th>DOB</th>
              <th>Entry Date</th>
              {/* Add more columns as needed */}
            </tr>
          </thead>
          <tbody>
            {MemberReport?.length > 0 ? (
              MemberReport.map((item, index) => (
                <tr key={index}>
                  <td>{item.memberId}</td>
                  <td>{item.memberName}</td>
                  <td>{item.addressName}</td>
                  <td>{item.typeName}</td>
                  <td>{item.email}</td>
                  <td>{item.phone}</td>
                  <td>{item.gender}</td>
                  <td>{item.dob}</td>
                  <td>{item.entryDate}</td>
                </tr>
              ))
            ) : (
              <tr>
                <td colSpan={8}>No data found.</td>
              </tr>
            )}
          </tbody>
        </table>
      </div>
    </div>
  );
}

export default MemberReport;
