import { ReactNode } from 'react';
import Table from 'react-bootstrap/Table';
import { IDataTableProps } from '../../shared/Models/dataTableProps';

const DataTable = <T extends object>(props: IDataTableProps<T> & { children?: ReactNode }) => {
    return (
        <Table responsive striped bordered hover variant="dark">
            <thead>
                <tr>
                    {props.headers.map((title) => 
                        <th key={title}>{title.toUpperCase().includes("URL") ? title.toUpperCase().replace("URL", '') : title.toUpperCase()}</th>
                    )}
                </tr> 
            </thead>
            <tbody>
                {props.data.map((element, index1) =>
                    <tr key={index1}>
                        {props.headers.map((title, index2) => 
                            <td key={index1.toString() + index2.toString()}>
                                {title.toUpperCase() === "COVERURL" ? <img src={`${element[title]}`} /> : (title.toUpperCase() === "IMDBURL" ? <a href={element[title]}>{element[title]}</a> : <>{element[title]}</>)}
                            </td>
                        )}
                    </tr>
                )}
            </tbody>
        </Table>
    )
}

export default DataTable
