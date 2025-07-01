// pages/signup.js
import { useEffect, useState } from 'react';

export default function SignupPage() {
  const [data, setData] = useState([]);
  const [id, setId] = useState('');
  const [loading, setLoading] = useState(false);

  const fetchData = async () => {
    if (!id) return;

    setLoading(true);
    try {
      const res = await fetch(`http://localhost:5000/GetSignup?id=${id}`);
      const result = await res.json();
      setData(result);
    } catch (error) {
      console.error('Error fetching data:', error);
      setData([]);
    } finally {
      setLoading(false);
    }
  };

  return (
    <div style={{ padding: '2rem' }}>
      <h2>Signup Details</h2>

      <div style={{ marginBottom: '1rem' }}>
        <input
          type="number"
          value={id}
          onChange={(e) => setId(e.target.value)}
          placeholder="Enter Signup ID"
        />
        <button onClick={fetchData} style={{ marginLeft: '10px' }}>
          Fetch
        </button>
      </div>

      {loading && <p>Loading...</p>}

      {data.length > 0 && (
        <table border="1" cellPadding="10">
          <thead>
            <tr>
              {Object.keys(data[0]).map((col) => (
                <th key={col}>{col}</th>
              ))}
            </tr>
          </thead>
          <tbody>
            {data.map((row, i) => (
              <tr key={i}>
                {Object.values(row).map((value, j) => (
                  <td key={j}>{value === null ? '' : value.toString()}</td>
                ))}
              </tr>
            ))}
          </tbody>
        </table>
      )}

      {!loading && data.length === 0 && <p>No data found</p>}
    </div>
  );
}
