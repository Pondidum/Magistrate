var ContentArea = React.createClass({

  render() {

    var actions = this.props.actions.map(function(action, index) {
      return (
        <div key={index} className="col-sm-2">{action}</div>
      );
    });

    var children  = this.props.children.map(function(child, index) {
      return (
        <li key={index} className="col-md-3">{child}</li>
      );
    });

    return (
      <div>
        <div className="row">
          {actions}
          <FilterBar filterChanged={this.props.filterChanged} />
        </div>
        <ul className="list-unstyled list-inline">
          {children}
        </ul>
      </div>
    );

  }

});
