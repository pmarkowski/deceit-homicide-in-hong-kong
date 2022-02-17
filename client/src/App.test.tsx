import { render, screen } from '@testing-library/react';
import { BrowserRouter } from 'react-router-dom';
import App from './App';

test('renders link to Deception game', () => {
  render(
    <BrowserRouter>
      <App />
    </BrowserRouter>);
  const linkElement = screen.getByText(/Deception: Murder in Hong Kong/i);
  expect(linkElement).toBeInTheDocument();
});
