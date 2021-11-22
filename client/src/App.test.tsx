import React from 'react';
import { render, screen } from '@testing-library/react';
import App from './App';

test('renders link to Deception game', () => {
  render(<App />);
  const linkElement = screen.getByText(/Deception: Murder in Hong Kong/i);
  expect(linkElement).toBeInTheDocument();
});
