import Pagination from 'react-bootstrap/Pagination';
import "./CustomPagination.css"
import { ReactNode } from 'react';
import { ICustomPaginationProps } from '../../shared/Models/customPaginationProps';
import { IPagination } from '../../shared/Models/pagination';

const CustomPagination = <T extends object>(props: ICustomPaginationProps<T> & { children?: ReactNode }) => {
    return (
        <Pagination>
            <Pagination.First />
            <Pagination.Prev />
            {Array.from({ length: Math.ceil(props.paginationData.count / props.paginationData.pageSize) }).map((_, index) =>
                <Pagination.Item active={index + 1 === props.paginationData.pageIndex} key={index} onClick={(event) => {
                    event.preventDefault();
                    let newPaginationData: IPagination<T> = { ...props.paginationData, pageIndex: index + 1 };
                    props.setPagination(newPaginationData);
                    props.setChangeFlag(!props.flag);
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
