import { Button } from 'react-bootstrap'
import Form from 'react-bootstrap/Form'

const SearchForm = () => {
  return (
      <Form className='d-flex justify-content-between'>
          <Form.Control type="text" placeholder="Normal text" className="text-light bg-dark" />
          <Button variant="success" className="px-3 px-md-5 py-1" style={{ marginLeft: "2em" }}>Search</Button>
      </Form>
  )
}

export default SearchForm
