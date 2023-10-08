import React, { useState } from 'react';
import { Container } from 'react-bootstrap'
import { Routes, Route } from 'react-router-dom';
import './App.css'
import Navbar from './components/Navbar/Navbar';
import Home from './pages/Home';
import { ThreeCircles } from 'react-loader-spinner';
const LazyMovies = React.lazy(() => import('./pages/Movies'));

function App() {
  const [isLoading, setIsLoading] = useState(false);

  return (
    <>
      <ThreeCircles
        height="100%"
        width="20%"
        color="rgba(40, 42, 58, 0.7)"
        wrapperStyle={{}}
        wrapperClass="threee-circles-wrapper"
        visible={isLoading}
        ariaLabel="three-circles-rotating"
        outerCircleColor=""
        innerCircleColor=""
        middleCircleColor=""
      />
      <Container fluid className={`bg-secondary min-vh-100 ${isLoading && "blocked-background"}`}>
        <Navbar />
        <Routes>
          <Route path='/' element={<Home />} />
          <Route path='/movies' element={<React.Suspense fallback="Loading..."><LazyMovies setSpinner={setIsLoading}/></React.Suspense>} />
        </Routes>
      </Container>
    </>
  )
}

export default App
