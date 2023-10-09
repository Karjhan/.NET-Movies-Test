import { useEffect, useState } from 'react';
import { IPagination } from '../shared/Models/pagination';
import { IMovie } from '../shared/Models/movie';
import DataTable from '../components/DataTable/DataTable';
import CustomPagination from '../components/Pagination/CustomPagination';
import Row from 'react-bootstrap/Row';
import Col from 'react-bootstrap/Col';
import { IMovieFetchParams } from '../shared/Models/movieFetchParams';
import fetchMovies from '../shared/Utils/fetchMovies';

const defaultMovieFetchParams: IMovieFetchParams = { pageIndex: 1, pageSize: 5};

const Movies = (props: { setSpinner: (arg0: boolean) => void; }) => {
  const [moviesData, setMoviesData] = useState<IPagination<Array<IMovie>>>();
  const [headers, setHeaders] = useState<Array<string>>();
  const [movieDataParams, setMovieDataParams] = useState<IMovieFetchParams>(defaultMovieFetchParams);

  useEffect(() => {
    const fetchMoviesData = () => {
      props.setSpinner(true);
      setTimeout(async () => { 
        const moviesDataObject = await fetchMovies(movieDataParams);
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
  }, [movieDataParams])

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
            <CustomPagination paginationData={moviesData} setPagination={setMovieDataParams} paginationParams={movieDataParams}/>
          </Col>
        </Row>
      </>}
    </>
  )
}

export default Movies
