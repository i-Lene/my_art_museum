export function Button({ onClick, children, disabled = false }) {
  return (
    <button onClick={onClick} disabled={disabled} className="custom-button">
      {children}
    </button>
  );
}
