import Form from 'react-bootstrap/Form';
import Button from 'react-bootstrap/Button';
import Row from 'react-bootstrap/Row';
import Col from 'react-bootstrap/Col';
import { useEffect, useState } from "react";
import DatePicker from "react-datepicker";
import Modal from 'react-bootstrap/Modal';

function CreateSalesOrder() {
    const [startDate, setStartDate] = useState();
    const [customers, setCustomers] = useState([]);
    const [show, setShow] = useState(false);

    const handleClose = () => setShow(false);
    const handleShow = () => setShow(true);

    useEffect(() => {
        setCustomerDropdownData();
    }, []);

    async function setCustomerDropdownData() {
        const response = await fetch('api/dropdown/customer');
        if (response.ok) {
            const data = await response.json();
            setCustomers(data);
        }
    }

    return (
        <div>
            <Modal show={show} onHide={handleClose}>
                <Modal.Header closeButton>
                    <Modal.Title>Add Item</Modal.Title>
                </Modal.Header>
                <Modal.Body>Woohoo, you are reading this text in a modal!</Modal.Body>
                <Modal.Footer>
                    <Button variant="secondary" onClick={handleClose}>
                        Close
                    </Button>
                    <Button variant="primary" onClick={handleClose}>
                        Save Changes
                    </Button>
                </Modal.Footer>
            </Modal>

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
                            <option value={0}>Select One</option>
                            {customers.map(customer => (
                                <option key={customer.id} value={customer.id}>{customer.label}</option>
                            ))}
                        </Form.Select>
                    </Form.Group>
                </Row>
                <Row className="mt-3 text-start">

                    <Form.Group as={Col} className='d-flex flex-column' controlId="formGridDate">
                        <Form.Label>Date</Form.Label>
                        <DatePicker className='form-control' selected={startDate ? startDate : null} onChange={(date) => setStartDate(date)} />
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
                    <Button variant="primary" onClick={handleShow}>
                        Add Item
                    </Button>
                </div>
                <div className='container mt-4'>
                    <table className="table table-striped">
                        <thead className="table-dark">
                            <tr>
                                <th>No</th>
                                <th>Action</th>
                                <th>Item Name</th>
                                <th>Qty</th>
                                <th>Price</th>
                                <th>Total</th>
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