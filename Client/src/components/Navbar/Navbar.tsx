import Row from "react-bootstrap/Row";
import Col from "react-bootstrap/Col";
import Container from "react-bootstrap/esm/Container";
import Nav from 'react-bootstrap/Nav';
import Navbar from 'react-bootstrap/Navbar';
import Offcanvas from 'react-bootstrap/Offcanvas';
import { Link } from "react-router-dom";
import "./Navbar.css"

const NavbarMain = () => {
  return (
    <Row>
      <Col className="p-0">
        <Navbar bg="dark" data-bs-theme="dark" key="lg" expand="lg" className="bg-body-tertiary mb-3">
          <Container fluid>
            <Navbar.Brand className="px-3 px-md-5">Movies-Test</Navbar.Brand>
            <Navbar.Toggle aria-controls="offcanvasNavbar-expand-lg" />
            <Navbar.Offcanvas
              id="offcanvasNavbar-expand-lg"
              aria-labelledby="offcanvasNavbarLabel-expand-lg"
              placement="end"
              data-bs-theme="dark"
            >
              <Offcanvas.Header closeButton>
                <Offcanvas.Title id="offcanvasNavbarLabel-expand-lg">
                  Movies-Test
                </Offcanvas.Title>
              </Offcanvas.Header>
              <Offcanvas.Body>
                <Nav className="justify-content-start flex-grow-1 pe-3">
                  <Link to="/" className="px-0 px-md-2 navlink">Home</Link>
                  <Link to="/movies" className="px-0 px-md-2 navlink">Movies</Link>
                </Nav>
              </Offcanvas.Body>
            </Navbar.Offcanvas>
          </Container>
        </Navbar>
      </Col>
    </Row>
  )
}

export default NavbarMain
