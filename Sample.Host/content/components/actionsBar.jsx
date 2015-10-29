var ActionsBar = React.createClass({

  render() {
    return (
      <div className={this.props.className}>
        <ul className="list-unstyled list-inline">
          <li><a href="#" className="btn btn-primary">Create User</a></li>
          <li><a href="#" className="btn btn-default">Add Permission</a></li>
          <li><a href="#" className="btn btn-default">Add Role</a></li>
        </ul>
      </div>
    );
  }

});
