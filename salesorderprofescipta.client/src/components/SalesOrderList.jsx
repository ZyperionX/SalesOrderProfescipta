import Form from 'react-bootstrap/Form';
import Button from 'react-bootstrap/Button';
import Row from 'react-bootstrap/Row';
import Col from 'react-bootstrap/Col';
import { useState } from "react";
import DatePicker from "react-datepicker";

import "react-datepicker/dist/react-datepicker.css";
function SalesOrderList() {
    const [startDate, setStartDate] = useState(new Date());
    return (
        <>
            <div className='container border rounded-3 mt-5 border-dark text-start p-4'>
                <Form>
                    <Row className="mb-3">
                        <Form.Group as={Col} controlId="formGridKeyword">
                            <Form.Label>Keyword</Form.Label>
                            <Form.Control placeholder="Input Here" />
                        </Form.Group>

                        <Form.Group as={Col} className='d-flex flex-column' controlId="formGridDate">
                            <Form.Label>Date</Form.Label>
                            <DatePicker className='form-control' selected={null} onChange={(date) => setStartDate(date)} />
                        </Form.Group>
                    
                    </Row>

                    <Button variant="dark" type="submit">
                        Search
                    </Button>
                </Form>
            </div>

            <div className='container d-flex flex-row mt-4'>
                <a href="/create-sales-order" className="btn btn-primary mx-2">Add New Data</a>
                <a href="/" className="btn btn-info mx-2">Export to Excel</a>
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
                        <tr>
                            <th>No</th>
                            <th>Action</th>
                            <th>Sales Order</th>
                            <th>Order Date</th>
                            <th>Customer</th>
                        </tr>
                        <tr>
                            <th>No</th>
                            <th>Action</th>
                            <th>Sales Order</th>
                            <th>Order Date</th>
                            <th>Customer</th>
                        </tr>
                        <tr>
                            <th>No</th>
                            <th>Action</th>
                            <th>Sales Order</th>
                            <th>Order Date</th>
                            <th>Customer</th>
                        </tr>
                    </tbody>
                </table>
            </div>
        </>
        
    );
}

export default SalesOrderList;