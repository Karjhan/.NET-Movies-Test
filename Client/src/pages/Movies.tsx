import { useEffect, useState } from 'react';
import { IPagination } from '../shared/Models/pagination';
import { IMovie } from '../shared/Models/movie';
import DataTable from '../components/DataTable/DataTable';
import CustomPagination from '../components/Pagination/CustomPagination';
import Row from 'react-bootstrap/Row';
import Col from 'react-bootstrap/Col';

const Movies = (props: { setSpinner: (arg0: boolean) => void; }) => {
  const [moviesData, setMoviesData] = useState<IPagination<Array<IMovie>>>();
  const [headers, setHeaders] = useState<Array<string>>();
  const [changePagination, setChangePagination] = useState<boolean>(false);

  useEffect(() => {
    const fetchMoviesData = () => {
      props.setSpinner(true);
      setTimeout(async () => { 
        const incomingJson = await fetch(`${import.meta.env.VITE_BACKEND_API_URL}/Movies?PageIndex=${moviesData === undefined ? 1 : moviesData.pageIndex}&PageSize=${moviesData === undefined ? 5 : moviesData.pageSize}`, {
          headers: {
            "Content-Type": "application/json",
            "Access-Control-Allow-Origin": "*"
          },
        });
        const moviesDataObject = await incomingJson.json();
        setMoviesData(moviesDataObject);
        let allKeys: Array<string> = Object.keys(moviesDataObject.data[0]);
        const idIndex: number = allKeys.indexOf("id");
        allKeys.splice(idIndex, 1);
        setHeaders(allKeys);
        props.setSpinner(false);
      }, 1000)
    }
    fetchMoviesData();
    return;
  }, [changePagination])

  return (
    <>
      {(moviesData?.data && headers) &&
      <>
        <Row>
          <Col>
            <DataTable data={moviesData.data} headers={headers} />
          </Col>
        </Row>
        <Row>
          <Col className="d-flex justify-content-center mt-4">
            <CustomPagination paginationData={moviesData} setPagination={setMoviesData} setChangeFlag={setChangePagination} flag={changePagination}/>
          </Col>
        </Row>
      </>}
    </>
  )
}

export default Movies
