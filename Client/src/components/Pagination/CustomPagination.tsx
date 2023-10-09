import Pagination from 'react-bootstrap/Pagination';
import "./CustomPagination.css"
import { ReactNode } from 'react';
import { ICustomPaginationProps } from '../../shared/Models/customPaginationProps';
import { IMovieFetchParams } from '../../shared/Models/movieFetchParams';

const CustomPagination = <T extends object>(props: ICustomPaginationProps<T> & { children?: ReactNode }) => {
    return (
        <Pagination>
            <Pagination.First />
            <Pagination.Prev />
            {Array.from({ length: Math.ceil(props.paginationData.count / props.paginationData.pageSize) }).map((_, index) =>
                <Pagination.Item active={index + 1 === props.paginationData.pageIndex} disabled={index + 1 === props.paginationData.pageIndex} key={index} onClick={(event) => {
                    event.preventDefault();
                    let newMovieFetchParams: IMovieFetchParams = { ...props.paginationParams, pageIndex: index+1 };
                    props.setPagination(newMovieFetchParams);
                }}>
                    {index+1}
                </Pagination.Item>
            )}
            <Pagination.Next />
            <Pagination.Last />
        </Pagination>
    )
}

export default CustomPagination
