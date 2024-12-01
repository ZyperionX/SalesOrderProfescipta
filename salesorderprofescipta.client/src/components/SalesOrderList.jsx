import Form from 'react-bootstrap/Form';
import Button from 'react-bootstrap/Button';
import Row from 'react-bootstrap/Row';
import Col from 'react-bootstrap/Col';
import { useEffect, useState } from "react";
import DatePicker from "react-datepicker";
import axios from 'axios';
import { saveAs } from 'file-saver';

import "react-datepicker/dist/react-datepicker.css";
function SalesOrderList() {
    const [startDate, setStartDate] = useState();
    const [salesOrders, setSalesOrders] = useState([]);
    const [keyword, setKeyword] = useState('');
    const [query, setQuery] = useState('');

    useEffect(() => {
        setSalesOrderData();
    }, [query]);

    async function setSalesOrderData() {
        const response = await fetch('api/salesorder?' + query);
        if (response.ok) {
            const data = await response.json();
            setSalesOrders(data);
        }
    }

    function convertToDate(date) {
        const convertedDate = new Date(date);
        const stringDate = convertedDate.getFullYear() + "-" + (convertedDate.getMonth()+1) + "-" + convertedDate.getDate();
        return stringDate;
    }

    const handleDownload = async () => {
        let instance = axios.create({ baseURL: "https://localhost:7236" });
        let options = {
            url: 'api/salesorder/excel',
            method: 'get',
            responseType: 'blob' // don't forget this
        };
        return instance.request(options)
            .then(response => {
                let filename = response.headers['content-disposition']
                    .split(';')
                    .find((n) => n.includes('filename='))
                    .replace('filename=', '')
                    .trim();
                let url = window.URL
                    .createObjectURL(new Blob([response.data]));
                saveAs(url, filename);
            });
    }

    const handleQuery = () => {
        if (keyword === '') {
            setQuery(new URLSearchParams({
                orderDate: convertToDate(startDate),
            }).toString());
        } else {
            setQuery(new URLSearchParams({
                keyword: keyword,
                orderDate: convertToDate(startDate),
            }).toString());
        }
    }
    function tableContent() {
        let i = 0;
        const list = []

        salesOrders.forEach((salesOrder) => {
            list.push(
                <tr key={i}>
                    <th>{i+1}</th>
                    <th>Action</th>
                    <th>{salesOrder.code}</th>
                    <th>{salesOrder.releaseDate}</th>
                    <th>{salesOrder.customerName}</th>
                </tr>
            );
            i++;
        })

        return (
            <>
                {list}
            </>
        )
    }

    return (
        <>
            <div className='container border rounded-3 mt-5 border-dark text-start p-4'>
                <Form>
                    <Row className="mb-3">
                        <Form.Group as={Col} controlId="formGridKeyword">
                            <Form.Label>Keyword</Form.Label>
                            <Form.Control placeholder="Input Here" as="input" value={keyword} onChange={(e) => setKeyword(e.target.value)} />
                        </Form.Group>

                        <Form.Group as={Col} className='d-flex flex-column' controlId="formGridDate">
                            <Form.Label>Date</Form.Label>
                            <DatePicker className='form-control' selected={startDate ? startDate : null} onChange={(date) => setStartDate(date)} />
                        </Form.Group>
                    
                    </Row>

                    <Button variant="dark" onClick={handleQuery}>
                        Search
                    </Button>
                </Form>
            </div>

            <div className='container d-flex flex-row mt-4'>
                <a href="/create-sales-order" className="btn btn-primary mx-2">Add New Data</a>
                {/*<Button onClick={ } className="btn btn-info mx-2">Export to Excel</Button>*/}
                <Button className="btn btn-info mx-2" onClick={handleDownload}>
                    Export to Excel
                </Button>
            </div>

            <div className='container mt-4'>
                <table className="table table-striped">
                    <thead className="table-dark">
                        <tr>
                            <th>No</th>
                            <th>Action</th>
                            <th>Sales Order</th>
                            <th>Order Date</th>
                            <th>Customer</th>
                        </tr>
                    </thead>
                    <tbody className="table-light">
                        {salesOrders && tableContent()}
                    </tbody>
                </table>
            </div>
        </>
        
    );
}

export default SalesOrderList;