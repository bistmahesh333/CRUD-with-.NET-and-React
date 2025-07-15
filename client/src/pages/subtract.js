import { useState } from "react";

export default function SubtractTwo() {
  const [N1, setN1] = useState('');
  const [N2, setN2] = useState('');
  const [sub, setSub] = useState(null);  // Changed 'sum' to 'sub'

  const subtract = () => {
    const result = Number(N1) - Number(N2);
    setSub(result);
  };

  return (
    <div>
      <input
        type="number"
        value={N1}
        onChange={(e) => setN1(e.target.value)}
        placeholder="Enter first number"
      />
      <input
        type="number"
        value={N2}
        onChange={(e) => setN2(e.target.value)}
        placeholder="Enter second number"
        style={{ marginLeft: '10px' }}
      />

      <button onClick={subtract} style={{ marginLeft: '10px' }}>
        Subtract
      </button>

      {sub !== null && (
        <p>Difference: {sub}</p>  // Changed text from 'Sum' to 'Difference'
      )}
    </div>
  );
}
