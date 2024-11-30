import Form from 'react-bootstrap/Form';
import Button from 'react-bootstrap/Button';
import Row from 'react-bootstrap/Row';
import Col from 'react-bootstrap/Col';
import { useState } from "react";
import DatePicker from "react-datepicker";

function CreateSalesOrder() {
    const [startDate, setStartDate] = useState(new Date());
    return (
        <div>
            <h3 className="fw-bold bg-dark text-white mt-1 py-2">Sales Order Information</h3>
            <div className="container">
                <Row className="mt-3 text-start">
                    <Form.Group as={Col} controlId="formGridSalesOrderCode">
                        <Form.Label>Sales Order Number</Form.Label>
                        <Form.Control placeholder="Input Here" />
                    </Form.Group>

                    <Form.Group as={Col} controlId="formGridSalesOrderCode">
                        <Form.Label>Customer</Form.Label>
                        <Form.Select aria-label="Default select example">
                            <option>Select One</option>
                            <option value="1">One</option>
                            <option value="2">Two</option>
                            <option value="3">Three</option>
                        </Form.Select>
                    </Form.Group>
                </Row>
                <Row className="mt-3 text-start">

                    <Form.Group as={Col} className='d-flex flex-column' controlId="formGridDate">
                        <Form.Label>Date</Form.Label>
                        <DatePicker className='form-control' selected={null} onChange={(date) => setStartDate(date)} />
                    </Form.Group>

                    <Form.Group as={Col} className="mb-3" controlId="formGridAddress">
                        <Form.Label>Address</Form.Label>
                        <Form.Control as="textarea" rows={3} />
                    </Form.Group>

                </Row>
            </div>

            <div>
                <h3 className="fw-bold bg-dark text-white mt-1 py-2">Detail Item Information</h3>
                <div className='container d-flex flex-row mt-4'>
                    <a href="/create-sales-order" className="btn btn-primary mx-2">Add Item</a>
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
            </div>
        </div>
    );
}

export default CreateSalesOrder;